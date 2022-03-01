using SMS.Base.Enums;
using System;

namespace SMS.Model.Dtos
{
    public class JobRecordDto
    {
        public Guid Id { get; set; }

        public SchedulerTypeEnum SchedulerType { get; set; }

        public Guid ObjectId { get; set; }

        public JobTypeEnum Type { get; set; }

        public JobStatusEnum Status { get; set; }

        public DateTimeOffset RunAt { get; set; }
    }
}