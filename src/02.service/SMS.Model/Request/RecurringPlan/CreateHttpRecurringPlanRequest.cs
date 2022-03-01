using SMS.Model.Dtos;
using SMS.Base.Enums;
using System;

namespace SMS.Model.Request
{
    public class CreateHttpRecurringPlanRequest
    {
        public Guid ApplicationId { get; set; }

        /// <example>RecurringPlanSample</example>
        public string Name { get; set; }

        /// <example>RecurringPlanSample</example>
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        /// <example>* * * * *</example>
        public string Cron { get; set; }

        public JobTypeEnum Type { get; set; }

        public HttpJobDto HttpParameter { get; set; }

        public RetryDto Retry { get; set; }
    }
}