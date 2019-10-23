using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErrorLoggerData.Entities
{
    [Table("ExceptionLog")]
    public class EntityExceptionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ExceptionLogId { get; set; }
        public string ApplicationName { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public int HResult { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        public EntityInnerExceptionLog InnerExceptionLog { get; set; }
    }
}
