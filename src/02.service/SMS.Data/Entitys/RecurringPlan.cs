using System;
using Common.VNextFramework.EntityFramework;
using SMS.Base.Enums;

namespace SMS.Data.Entitys
{
    public class RecurringPlan : SequentialGuidEntity
    {
        public Guid ApplicationId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Cron { get; set; }

        public JobTypeEnum Type { get; set; }

        public object Parameter { get; set; }

        public RetryInfo Retry { get; set; }

        public RecurringPlanStatusEnum Status { get; set; }

        public int RunCount { get; set; }

        public Guid? LastJobRecordId { get; set; }

        public DateTimeOffset? LastFireAt { get; set; }
    }
}