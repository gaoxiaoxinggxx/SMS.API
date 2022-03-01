using System;
using SMS.Base.Enums;
using SMS.Data.Entitys;

namespace SMS.Service.Hangfire
{
    public class SchedulerContext
    {
        public SchedulerContext(SchedulerTypeEnum schedulerType, Guid objectId, Guid applicationId)
        {
            SchedulerType = schedulerType;
            ObjectId = objectId;

            ApplicationId = applicationId;
        }

        public SchedulerTypeEnum SchedulerType { get; }

        /// <summary>
        ///     SchedulerType = RecurringPlan is RecurringPlanId,
        ///     SchedulerType = DelayedJob is DelayedJobId
        /// </summary>
        public Guid ObjectId { get; }

        public Guid ApplicationId { get; set; }

        public RetryInfo Retry { get; set; } = new RetryInfo();
    }
}