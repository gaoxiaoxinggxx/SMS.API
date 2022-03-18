using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commen.ExceptionHandling
{
    public class RemoteServiceErrorInfo
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RemoteServiceValidationErrorInfo[] ValidationErrors { get; set; }

        public RemoteServiceErrorInfo()
        {
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Details { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public IDictionary Data { get; set; }

        public RemoteServiceErrorInfo(string message, object[] errors = null, string code = null)
        {
            Message = message;
            Code = code;
            Errors = errors;
        }
    }

    public class RemoteServiceValidationErrorInfo
    {
        public string Message { get; set; }

        public string[] Members { get; set; }

        public RemoteServiceValidationErrorInfo()
        {

        }

        public RemoteServiceValidationErrorInfo(string message)
        {
            Message = message;
        }

        public RemoteServiceValidationErrorInfo(string message, string[] members)
            : this(message)
        {
            Members = members;
        }

        public RemoteServiceValidationErrorInfo(string message, string member)
            : this(message, new[] { member })
        {

        }
    }
}
