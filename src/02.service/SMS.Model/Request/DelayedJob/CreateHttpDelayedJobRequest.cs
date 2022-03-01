using SMS.Model.Dtos;
using System;

namespace SMS.Model.Request.DelayedJob
{
    /// <summary>
    ///     Create Http Delayed Job Request
    /// </summary>
    public class CreateHttpDelayedJobRequest
    {
        /// <summary>
        ///     Application Id
        /// </summary>
        public Guid ApplicationId { get; set; } = default!;

        /// <example>HttpDelayedJobSample</example>
        public string Name { get; set; } = null!;

        /// <example>Http DelayedJob Sample</example>
        public string Description { get; set; }

        /// <summary>
        ///     http parameter
        /// </summary>
        public HttpJobDto HttpInfo { get; set; } = new HttpJobDto();

        /// <summary>
        ///     retry info
        /// </summary>
        public RetryDto Retry { get; set; }

        /// <summary>
        ///     Run Time
        /// </summary>
        public DateTimeOffset? ExpireAt { get; set; }
    }
}