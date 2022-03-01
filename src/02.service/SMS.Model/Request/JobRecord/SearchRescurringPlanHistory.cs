using SMS.Base.Enums;
using System;

namespace SMS.Model.Request.JobRecord
{
    public class SearchRecurringPlanHistory
    {
        public Guid ApplicationId { get; set; }

        public Guid? RecurringPlanId { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public JobStatusEnum? Status { get; set; }
    }
}