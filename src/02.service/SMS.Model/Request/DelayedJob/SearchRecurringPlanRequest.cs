using SMS.Base.Enums;
using System;

namespace SMS.Model.Request.DelayedJob
{
    public class SearchDelayedJobRequest
    {
        public Guid ApplicationId { get; set; }

        public JobStatusEnum? Status { get; set; }

        public string Name { get; set; }

        public JobTypeEnum? Type { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}