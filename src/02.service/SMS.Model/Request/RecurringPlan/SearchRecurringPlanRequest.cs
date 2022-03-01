using System;
using SMS.Base.Enums;
using BCChina.VNextFramework.Model;

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