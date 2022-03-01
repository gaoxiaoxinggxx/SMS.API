using System;
using SMS.Model.Dtos;
using SMS.Base.Enums;

namespace BC.CSM.Response.RecurringPlan
{
    public class GetByIdRecurringPlanResponse
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }

        /// <example>RecurringPlanSample</example>
        public string Name { get; set; }

        /// <example>RecurringPlanSample</example>
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        /// <example>0 * * * * ? *</example>
        public string Cron { get; set; }

        public JobTypeEnum Type { get; set; }

        public HttpJobDto HttpParameter { get; set; }

        public RetryDto Retry { get; set; }
    }
}