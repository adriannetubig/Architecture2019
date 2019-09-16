using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationData.Entities
{
    public partial class EntityUser
    {
        [NotMapped]
        public string RoleName { get; set; }
    }
}
