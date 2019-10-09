using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamData.Entities
{
    [Table("Fibonacci")]
    public partial class EntityFibonacci
    {
        [Key]
        public int FibonacciId { get; set; }
        public int Iterations { get; set; }
        public int Total { get; set; }
    }
}
