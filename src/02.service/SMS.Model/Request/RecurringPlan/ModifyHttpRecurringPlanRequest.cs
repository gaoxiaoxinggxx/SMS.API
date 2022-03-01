using System;
using SMS.Model.Dtos;
using SMS.Base.Enums;

namespace SMS.Model.Request
{
    public class ModifyHttpRecurringPlanRequest
    {
        public Guid ApplicationId { get; set; }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Cron { get; set; }

        public JobTypeEnum Type { get; set; }

        public HttpJobDto HttpParameter { get; set; }

        public RetryDto Retry { get; set; }
    }
}