using System;
using System.Threading.Tasks;
using SMS.Model.Dtos;
using SMS.Base.Enums;
using SMS.Model.Request.JobRecord;

namespace SMS.Service.Interfaces
{
    public interface IJobRecordService
    {
        Task<Guid> CreateJobRecord(Guid applicationId, int innerId, SchedulerTypeEnum schedulerType, Guid objectId);

        Task<bool> ChangeStatus(Guid jobRecordId, JobStatusEnum newState, JobStatusEnum? oldState, bool isCompleted,
            bool hasException);

        Task<JobRecordDto> SearchDelayedJobHistory(SearchDelayedJobHistoryRequest search);

        Task<JobRecordDto> SearchRecurringPlanHistory(SearchRecurringPlanHistory search);

        Task<JobRecordWithStatesDto> GetDetail(Guid jobRecordId);
    }
}