﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.States;
using Hangfire.Storage;
using Newtonsoft.Json;

namespace SMS.Service.Hangfire
{
    public class MyContinuationsSupportAttribute : JobFilterAttribute, IElectStateFilter, IApplyStateFilter
    {
        private static readonly TimeSpan AddJobLockTimeout = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan ContinuationStateFetchTimeout = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan ContinuationInvalidTimeout = TimeSpan.FromMinutes(15);
        private readonly HashSet<string> _knownFinalStates;

        private readonly ILog _logger = LogProvider.For<ContinuationsSupportAttribute>();

        private readonly bool _pushResults;
        private readonly IBackgroundJobStateChanger _stateChanger;

        public MyContinuationsSupportAttribute()
            : this(false)
        {
        }

        public MyContinuationsSupportAttribute(bool pushResults)
            : this(pushResults, new HashSet<string> {DeletedState.StateName, SucceededState.StateName})
        {
        }

        public MyContinuationsSupportAttribute(HashSet<string> knownFinalStates)
            : this(false, knownFinalStates)
        {
        }

        public MyContinuationsSupportAttribute(bool pushResults, HashSet<string> knownFinalStates)
            : this(pushResults, knownFinalStates, new BackgroundJobStateChanger())
        {
        }

        public MyContinuationsSupportAttribute(
            [NotNull] HashSet<string> knownFinalStates,
            [NotNull] IBackgroundJobStateChanger stateChanger)
            : this(false, knownFinalStates, stateChanger)
        {
        }

        public MyContinuationsSupportAttribute(
            bool pushResults,
            [NotNull] HashSet<string> knownFinalStates,
            [NotNull] IBackgroundJobStateChanger stateChanger)
        {
            if (knownFinalStates == null) throw new ArgumentNullException(nameof(knownFinalStates));
            if (stateChanger == null) throw new ArgumentNullException(nameof(stateChanger));

            _pushResults = pushResults;
            _knownFinalStates = knownFinalStates;
            _stateChanger = stateChanger;

            // Ensure this filter is the last filter in the chain to start
            // continuations on the last candidate state only.
            Order = 1000;
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            var awaitingState = context.NewState as AwaitingState;
            if (awaitingState != null) context.JobExpirationTimeout = awaitingState.Expiration;
        }

        void IApplyStateFilter.OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }

        public void OnStateElection(ElectStateContext context)
        {
            var awaitingState = context.CandidateState as AwaitingState;
            if (awaitingState != null)
                // Branch for a child background job.
                AddContinuation(context, awaitingState);
            else if (_knownFinalStates.Contains(context.CandidateState.Name))
                // Branch for a parent background job.
                ExecuteContinuationsIfExist(context);
        }

        internal static List<Continuation> DeserializeContinuations(string serialized)
        {
            var continuations = SerializationHelper.Deserialize<List<Continuation>>(serialized);

            if (continuations != null && continuations.TrueForAll(x => x.JobId == null))
                continuations =
                    SerializationHelper.Deserialize<List<Continuation>>(serialized, SerializationOption.User);

            return continuations ?? new List<Continuation>();
        }

        private void AddContinuation(ElectStateContext context, AwaitingState awaitingState)
        {
            var connection = context.Connection;
            var parentId = awaitingState.ParentId;

            // We store continuations as a json array in a job parameter. Since there 
            // is no way to add a continuation in an atomic way, we are placing a 
            // distributed lock on parent job to prevent race conditions, when
            // multiple threads add continuation to the same parent job.
            using (connection.AcquireDistributedJobLock(parentId, AddJobLockTimeout))
            {
                var jobData = connection.GetJobData(parentId);
                if (jobData == null)
                    // When we try to add a continuation for a removed job,
                    // the system should throw an exception instead of creating
                    // corrupted state.
                    throw new InvalidOperationException(
                        $"Can not add a continuation: parent background job '{parentId}' does not exist.");

                var continuations = GetContinuations(connection, parentId);

                // Continuation may be already added. This may happen, when outer transaction
                // was failed after adding a continuation last time, since the addition is
                // performed outside of an outer transaction.
                if (!continuations.Exists(x => x.JobId == context.BackgroundJob.Id))
                {
                    continuations.Add(new Continuation
                        {JobId = context.BackgroundJob.Id, Options = awaitingState.Options});

                    // Set continuation only after ensuring that parent job still
                    // exists. Otherwise we could create add non-expiring (garbage)
                    // parameter for the parent job.
                    SetContinuations(connection, parentId, continuations);
                }

                var currentState = connection.GetStateData(parentId);

                if (currentState != null && _knownFinalStates.Contains(currentState.Name))
                {
                    var startImmediately =
                        !awaitingState.Options.HasFlag(JobContinuationOptions.OnlyOnSucceededState) ||
                        currentState.Name == SucceededState.StateName;

                    if (_pushResults && currentState.Data.TryGetValue("Result", out var antecedentResult))
                        context.Connection.SetJobParameter(context.BackgroundJob.Id, "AntecedentResult",
                            antecedentResult);

                    context.CandidateState = startImmediately
                        ? awaitingState.NextState
                        : new DeletedState {Reason = "Continuation condition was not met"};
                }
            }
        }

        private void ExecuteContinuationsIfExist(ElectStateContext context)
        {
            // The following lines are executed inside a distributed job lock,
            // so it is safe to get continuation list here.
            var continuations = GetContinuations(context.Connection, context.BackgroundJob.Id);
            var nextStates = new Dictionary<string, IState>();


            _logger.Debug(
                $"jobId = {context.BackgroundJob.Id}, current state = {context.CurrentState},candidate state = {context.CandidateState.Name}");
            _logger.Debug($"continuations = {JsonConvert.SerializeObject(continuations)}");

            // Getting continuation data for all continuations – state they are waiting 
            // for and their next state.
            foreach (var continuation in continuations)
            {
                _logger.Debug($"jobId = {context.BackgroundJob.Id}, continuationJobId = {continuation.JobId}");

                if (string.IsNullOrWhiteSpace(continuation.JobId)) continue;

                var currentState = GetContinuationState(context, continuation.JobId, ContinuationStateFetchTimeout);
                if (currentState == null)
                {
                    _logger.Debug(
                        $"jobId = {context.BackgroundJob.Id}, continuationJobId = {continuation.JobId}, currentState == null");
                    continue;
                }

                // All continuations should be in the awaiting state. If someone changed 
                // the state of a continuation, we should simply skip it.
                if (currentState.Name != AwaitingState.StateName)
                {
                    _logger.Debug(
                        $"jobId = {context.BackgroundJob.Id}, continuationJobId = {continuation.JobId}, currentState.Name != AwaitingState.StateName");
                    continue;
                }

                IState nextState;

                if (continuation.Options.HasFlag(JobContinuationOptions.OnlyOnSucceededState) &&
                    context.CandidateState.Name != SucceededState.StateName)
                    nextState = new DeletedState {Reason = "Continuation condition was not met"};
                else
                    try
                    {
                        nextState = SerializationHelper.Deserialize<IState>(currentState.Data["NextState"],
                            SerializationOption.TypedInternal);
                    }
                    catch (Exception ex)
                    {
                        nextState = new FailedState(ex)
                        {
                            Reason = "An error occurred while deserializing the continuation"
                        };
                    }

                if (!nextStates.ContainsKey(continuation.JobId))
                {
                    _logger.Debug(
                        $"jobId = {context.BackgroundJob.Id}, continuationJobId = {continuation.JobId},  nextStates.Add(continuation.JobId, nextState);");
                    // Duplicate continuations possible, when they were added before version 1.6.10.
                    // Please see details in comments for the AddContinuation method near the line
                    // with checking for existence (continuations.Exists).
                    nextStates.Add(continuation.JobId, nextState);
                }
            }

            string antecedentResult = null;

            if (_pushResults)
            {
                var serializedData = context.CandidateState.SerializeData();
                serializedData.TryGetValue("Result", out antecedentResult);
            }

            foreach (var tuple in nextStates)
            {
                if (antecedentResult != null)
                    context.Connection.SetJobParameter(tuple.Key, "AntecedentResult", antecedentResult);

                _stateChanger.ChangeState(new StateChangeContext(
                    context.Storage,
                    context.Connection,
                    tuple.Key,
                    tuple.Value,
                    AwaitingState.StateName));
            }
        }

        private StateData GetContinuationState(ElectStateContext context, string continuationJobId, TimeSpan timeout)
        {
            StateData currentState = null;

            var started = Stopwatch.StartNew();
            var firstAttempt = true;

            while (true)
            {
                var continuationData = context.Connection.GetJobData(continuationJobId);
                if (continuationData == null)
                {
                    _logger.Warn(
                        $"Can not start continuation '{continuationJobId}' for background job '{context.BackgroundJob.Id}': continuation does not exist.");

                    break;
                }

                currentState = context.Connection.GetStateData(continuationJobId);
                if (currentState != null) break;

                if (DateTime.UtcNow - continuationData.CreatedAt > ContinuationInvalidTimeout)
                {
                    _logger.Warn(
                        $"Continuation '{continuationJobId}' has been ignored: it was deemed to be aborted, because its state is still non-initialized.");

                    break;
                }

                if (started.Elapsed >= timeout)
                {
                    _logger.Warn(
                        $"Can not start continuation '{continuationJobId}' for background job '{context.BackgroundJob.Id}': timeout expired while trying to fetch continuation state.");

                    break;
                }

                Thread.Sleep(firstAttempt ? 0 : 100);
                firstAttempt = false;
            }

            return currentState;
        }

        private static void SetContinuations(
            IStorageConnection connection, string jobId, List<Continuation> continuations)
        {
            connection.SetJobParameter(jobId, "Continuations", SerializationHelper.Serialize(continuations));
        }

        private static List<Continuation> GetContinuations(IStorageConnection connection, string jobId)
        {
            return DeserializeContinuations(connection.GetJobParameter(jobId, "Continuations"));
        }

        internal struct Continuation
        {
            public string JobId { get; set; }
            public JobContinuationOptions Options { get; set; }
        }
    }
}