using Common.VNextFramework.EntityFramework;
using SMS.Base.Enums;
using System;

namespace SMS.Data.Entitys
{
    public class DelayedJob : SequentialGuidEntity
    {
        public Guid ApplicationId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public JobTypeEnum Type { get; set; }

        public object Parameter { get; set; }

        public RetryInfo Retry { get; set; }

        public int RunCount { get; set; }

        public DateTimeOffset ExpireAt { get; set; }

        public Guid? LastJobRecordId { get; set; }

        public JobRecord? LastJobRecord { get; set; }
    }
}