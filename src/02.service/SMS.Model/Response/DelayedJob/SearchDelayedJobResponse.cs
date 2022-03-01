using System;
using SMS.Model.Dtos;
using SMS.Base.Enums;

namespace BC.CSM.Response.DelayedJob
{
    public class SearchDelayedJobResponse
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public string Name { get; set; }

        public JobTypeEnum Type { get; set; }

        public JobTypeParameter TypeParameter { get; set; }

        public RetryDto Retry { get; set; }

        public JobStatusEnum Status { get; set; }

        public int RunCount { get; set; }

        public DateTimeOffset ExpireAt { get; set; }

        public Guid? LastJobRecordId { get; set; }

        public bool IsCompleted { get; set; }

        public bool HasException { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}