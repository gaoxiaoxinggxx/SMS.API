using System;
using Common.VNextFramework.EntityFramework;
using SMS.Base.Enums;

namespace SMS.Data.Entitys
{
    public class JobRecord : SequentialGuidEntity
    {
        public Guid ApplicationId { get; set; }

        public SchedulerTypeEnum SchedulerType { get; set; }

        public Guid ObjectId { get; set; }

        /// <summary>
        ///     hangfire Job Id
        /// </summary>
        public int InnerId { get; set; }

        public JobStatusEnum Status { get; set; }

        public bool IsCompleted { get; set; }

        public bool HasException { get; set; }

        public DateTimeOffset RunAt { get; set; }
    }
}