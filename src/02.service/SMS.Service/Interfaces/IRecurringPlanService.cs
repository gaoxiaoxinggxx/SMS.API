using System;
using System.Threading.Tasks;
using SMS.Model.Request;
using SMS.Model.Request.RecurringPlan;
using BC.CSM.Response;
using BC.CSM.Response.RecurringPlan;

namespace SMS.Service.Interfaces
{
    public interface IRecurringPlanService
    {
        Task<CommonActionResponse> CreateHttpRecurringPlan(CreateHttpRecurringPlanRequest request);

        Task<CommonActionResponse>
            ModifyHttpRecurringPlan(Guid recurringPlanId, ModifyHttpRecurringPlanRequest request);

        Task<Guid> CreateJobRecord(Guid recurringPlanId, int innerId);

        Task<CommonSuccessResponse> Start(Guid recurringPlanId);

        Task<CommonSuccessResponse> Stop(Guid recurringPlanId);

        Task<CommonSuccessResponse> Trigger(Guid recurringPlanId);

        Task<CommonActionResponse> Delete(Guid recurringPlanId);

        Task<SearchRecurringPlanResponse> Search(SearchRecurringPlanRequest request);
        Task<GetByIdRecurringPlanResponse> GetById(Guid recurringPlanId);
    }
}