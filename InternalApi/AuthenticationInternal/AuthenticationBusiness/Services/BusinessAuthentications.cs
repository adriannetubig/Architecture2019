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

        public RequestResult<Authentication> Create(string refreshToken, User user)
        {
            var requestResult = new RequestResult<Authentication>();
            try
            {
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
            }
            catch (Exception e)
            {
                requestResult.Exceptions.Add(e);
            }

            return requestResult;
        }
    }
}
