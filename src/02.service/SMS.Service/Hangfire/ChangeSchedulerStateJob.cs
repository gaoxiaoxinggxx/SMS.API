using System;
using System.Threading.Tasks;
using SMS.Service.Interfaces;
using Hangfire;
using Hangfire.Server;
using SMS.Base.Enums;

namespace SMS.Service.Hangfire
{
    public class ChangeSchedulerStateJob
    {
        private readonly IDelayedJobService _delayedJobService;
        private readonly IJobRecordService _jobRecordService;
        private readonly IRecurringPlanService _recurringPlanService;

        public ChangeSchedulerStateJob(IDelayedJobService delayedJobService, IJobRecordService jobRecordService,
            IRecurringPlanService recurringPlanService)
        {
            _delayedJobService = delayedJobService;
            _jobRecordService = jobRecordService;
            _recurringPlanService = recurringPlanService;
        }

        [Queue("change_status")]
        public async Task Run(SchedulerContext schedulerContext, string jobId, HangfireState newState, string? oldState,
            PerformContext performContext)
        {
            var innerJobRecordId = performContext.Storage.GetConnection().GetJobParameter(jobId, "JobRecordId");
            if (schedulerContext.SchedulerType == SchedulerTypeEnum.DelayedJob)
                if (string.IsNullOrEmpty(innerJobRecordId))
                {
                    //new
                    var jobRecordId =
                        await _delayedJobService.CreateJobRecord(schedulerContext.ObjectId, int.Parse(jobId));
                    performContext.Connection.SetJobParameter(jobId, "JobRecordId", jobRecordId.ToString());
                    return;
                }

            if (schedulerContext.SchedulerType == SchedulerTypeEnum.RecurringPlan)
                if (string.IsNullOrEmpty(innerJobRecordId))
                {
                    //new
                    var jobRecordId =
                        await _recurringPlanService.CreateJobRecord(schedulerContext.ObjectId, int.Parse(jobId));
                    performContext.Connection.SetJobParameter(jobId, "JobRecordId", jobRecordId.ToString());
                    return;
                }

            var newStateEnum = ConvertToJobStatus(newState.Name);
            JobStatusEnum? oldStateEnum = null;
            if (!string.IsNullOrEmpty(oldState)) oldStateEnum = ConvertToJobStatus(oldState);

            _jobRecordService.ChangeStatus(Guid.Parse(innerJobRecordId), newStateEnum, oldStateEnum, newState.IsFinal,
                    newStateEnum == JobStatusEnum.Failed)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private JobStatusEnum ConvertToJobStatus(string stateName)
        {
            switch (stateName)
            {
                case "Awaiting": return JobStatusEnum.Awaiting;
                case "Enqueued": return JobStatusEnum.Awaiting;
                case "Processing": return JobStatusEnum.Processing;
                case "Scheduled": return JobStatusEnum.Scheduled;
                case "Succeeded": return JobStatusEnum.Succeeded;
                case "Failed": return JobStatusEnum.Failed;
                case "Deleted": return JobStatusEnum.Deleted;
                default:
                    throw new Exception("None know StateName:" + stateName);
            }
        }
    }
}