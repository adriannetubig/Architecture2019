using System;

namespace BaseModel
{
    public class JwtTokenValidation
    {
        public double ClockSkewMinutes { get; set; }
        public string IssuerSigningKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public TimeSpan ClockSkew => TimeSpan.FromMinutes(ClockSkewMinutes);
        public string[] AllowedOrigins { get; set; }
    }
}
