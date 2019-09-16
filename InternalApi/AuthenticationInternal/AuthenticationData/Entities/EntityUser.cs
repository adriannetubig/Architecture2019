using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationData.Entities
{
    [Table("User")]
    public class EntityUser
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [ForeignKey("RoleId")]
        public EntityRole Role { get; set; }
    }
}
