using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationData.Entities
{
    [Table("RefreshToken")]
    public partial class EntityRefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }

        public string Token { get; set; }
    }
}
