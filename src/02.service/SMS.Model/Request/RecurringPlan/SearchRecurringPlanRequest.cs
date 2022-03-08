using System;
using Common.VNextFramework.Model;
using SMS.Base.Enums;

namespace SMS.Model.Request.RecurringPlan
{
    public class SearchRecurringPlanRequest : PageQueryModel
    {
        public Guid ApplicationId { get; set; }

        public RecurringPlanStatusEnum? Status { get; set; }

        public string Name { get; set; }

        public JobTypeEnum? Type { get; set; }
    }
}