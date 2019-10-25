using Newtonsoft.Json;
using System;

namespace ErrorLoggerBusiness.Models
{
    public class ExceptionLog
    {
        public int ExceptionLogId { get; set; }
        public string ApplicationName { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public int HResult { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        [JsonProperty("InnerException")]
        public InnerExceptionLog InnerExceptionLog { get; set; }
    }
}
