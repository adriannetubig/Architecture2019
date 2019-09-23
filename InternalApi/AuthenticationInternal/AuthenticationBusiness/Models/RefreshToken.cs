namespace AuthenticationBusiness.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }

        public string Token { get; set; }
    }
}
