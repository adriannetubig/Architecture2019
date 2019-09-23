using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using BaseModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationBusiness.Services
{
    public class BusinessAuthentications : IBusinessAuthentications
    {
        private readonly JwtTokenValidation _jwtTokenValidation;
        private readonly JwtTokenSettings _jwtTokenSettings;
        public BusinessAuthentications(JwtTokenSettings jwtTokenSettings, JwtTokenValidation jwtTokenValidation)
        {
            _jwtTokenValidation = jwtTokenValidation;
            _jwtTokenSettings = jwtTokenSettings;
        }

        public RequestResult<Authentication> Create(string refreshToken, User user)//Todo: Possible NuGet Package
        {
            var requestResult = new RequestResult<Authentication>();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenValidation.IssuerSigningKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    //new Claim(ClaimTypes.Role, user.Role) No available role yet
                };

            var authentication = new Authentication
            {
                Expiration = _jwtTokenSettings.Expiration,
                InvalidBefore = DateTime.UtcNow,
                RefreshToken = refreshToken
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtTokenValidation.ValidIssuer,
                audience: _jwtTokenValidation.ValidAudience,
                claims: claims,
                notBefore: authentication.InvalidBefore,
                expires: authentication.Expiration,
                signingCredentials: signinCredentials
            );
            authentication.Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            requestResult.Model = authentication;

            return requestResult;
        }

        public RequestResult<User> VerifyToken(Authentication authentication)
        {
            var requestResult = new RequestResult<User>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _jwtTokenValidation.ValidIssuer,
                ValidAudience = _jwtTokenValidation.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenValidation.IssuerSigningKey)),
                ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_jwtTokenValidation.ClockSkewMinutes))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(authentication.Token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                requestResult.Errors.Add("Invalid Token");
            }
            else
            {
                var user = new User()
                {
                    UserId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    Username = principal.Identity.Name
                };
                requestResult.Model = user;
            }

            return requestResult;
        }
    }
}
