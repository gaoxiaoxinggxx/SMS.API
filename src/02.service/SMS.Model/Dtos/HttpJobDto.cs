using System.Collections.Generic;

namespace SMS.Model.Dtos
{
    /// <summary>
    ///     Http Job Parameter
    /// </summary>
    public class HttpJobDto : JobTypeParameter
    {
        /// <summary>
        ///     GET,POST,PUT,DELETE
        /// </summary>
        /// <example>GET</example>
        public string Method { get; set; }

        /// <summary>
        /// </summary>
        /// <example>https://www.baidu.com</example>
        public string Url { get; set; }

        /// <summary>
        ///     Content Type
        /// </summary>
        /// <example>text/html</example>
        public string ContentType { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public object Data { get; set; }
    }
}