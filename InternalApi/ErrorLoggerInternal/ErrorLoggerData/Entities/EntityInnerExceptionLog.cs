using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErrorLoggerData.Entities
{
    [Table("InnerExceptionLog")]
    public class EntityInnerExceptionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int InnerExceptionLogId { get; set; }
        public int ExceptionLogId { get; set; }
        public string ApplicationName { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public int HResult { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        [ForeignKey("ExceptionLogId")]
        public EntityExceptionLog EntityExceptionLog { get; set; }
    }
}
