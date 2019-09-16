using System;

namespace BaseModel
{
    public class JwtTokenSettings
    {
        public DateTime Expiration => DateTime.UtcNow.AddMinutes(ExpiresMinutes);
        public double ExpiresMinutes { get; set; }
    }
}
