using System;
using System.Collections.Generic;

namespace SMS.Model.Dtos
{
    /// <summary>
    ///     job history with status
    /// </summary>
    public class JobRecordWithStatesDto : JobRecordDto
    {
        /// <summary>
        ///     history status
        /// </summary>
        public List<JobRecordStateDto> HistoryStates { get; set; }
    }

    /// <summary>
    ///     job history state info
    /// </summary>
    public class JobRecordStateDto
    {
        /// <summary>
        ///     Job history detail state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     crated time
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     other info
        /// </summary>
        public IDictionary<string, string> Data { get; set; }
    }
}