using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationData.Entities
{
    [Table("Role")]
    public class EntityRole
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
