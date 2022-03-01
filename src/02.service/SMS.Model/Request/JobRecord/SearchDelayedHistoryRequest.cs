using SMS.Base.Enums;
using System;

namespace SMS.Model.Request.JobRecord
{
    public class SearchDelayedJobHistoryRequest
    {
        public Guid ApplicationId { get; set; }

        public Guid? DelayedJobId { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public JobStatusEnum? Status { get; set; }
    }
}