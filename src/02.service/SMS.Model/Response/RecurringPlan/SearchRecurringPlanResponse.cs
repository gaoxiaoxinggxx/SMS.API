using System;
using SMS.Model.Dtos;
using SMS.Base.Enums;

namespace BC.CSM.Response.RecurringPlan
{
    public class SearchRecurringPlanResponse
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Cron { get; set; }

        public JobTypeEnum Type { get; set; }

        public JobTypeParameter TypeParameter { get; set; }

        public RetryDto Retry { get; set; }

        public RecurringPlanStatusEnum Status { get; set; }

        public int RunCount { get; set; }

        public DateTimeOffset LastFireAt { get; set; }

        public DateTimeOffset NextFireAt { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}