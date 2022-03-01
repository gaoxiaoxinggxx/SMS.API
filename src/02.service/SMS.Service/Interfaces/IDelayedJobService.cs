using BC.CSM.Response;
using BC.CSM.Response.DelayedJob;
using SMS.Model.Request.DelayedJob;
using System;
using System.Threading.Tasks;

namespace SMS.Service.Interfaces
{
    public interface IDelayedJobService
    {
        Task<CommonActionResponse> CreateHttpJob(CreateHttpDelayedJobRequest request);

        Task<SearchDelayedJobResponse> Search(SearchDelayedJobRequest request);

        Task<Guid> CreateJobRecord(Guid delayedJobId, int innerId);

        Task<CommonSuccessResponse> Restart(Guid delayedJobId);
    }
}