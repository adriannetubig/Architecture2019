using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using BaseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationApi.Controllers.V1
{
    public class AuthenticationsController : BaseControllerV1
    {
        private readonly IBusinessAuthentications _iBusinessAuthentications;
        private readonly IBusinessRefreshTokens _iBusinessRefreshTokens;
        private readonly IBusinessUsers _iBusinessUsers;

        public AuthenticationsController(IBusinessAuthentications iBusinessAuthentications, IBusinessRefreshTokens iBusinessRefreshTokens, IBusinessUsers iBusinessUsers)
        {
            _iBusinessAuthentications = iBusinessAuthentications;
            _iBusinessRefreshTokens = iBusinessRefreshTokens;
            _iBusinessUsers = iBusinessUsers;
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Authenticate(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<Authentication>();

            var requestResultUser = await _iBusinessUsers.Authenticate(user, cancellationToken);
            requestResult.Add(requestResultUser);

            if (requestResultUser.Succeeded)
            {
                var requestRefreshToken = await _iBusinessRefreshTokens.Create(requestResultUser.Model.UserId, cancellationToken);
                requestResult.Add(requestRefreshToken);

                var requestResultAuthentication = _iBusinessAuthentications.Create(requestRefreshToken.Model.Token, requestResultUser.Model);
                requestResult.Add(requestResultAuthentication);

                if (requestResultAuthentication.Succeeded)
                    requestResult.Model = requestResultAuthentication.Model;

                return Ok(requestResult);
            }
            else
            {
                return Unauthorized(requestResult);
            }

        }

        [AllowAnonymous, HttpPost, Route("Refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken, [FromBody]Authentication authentication)
        {
            var requestResult = new RequestResult<Authentication>();
            User user;

            var requestResultVerifyToken = _iBusinessAuthentications.VerifyToken(authentication);

            requestResult.Add(requestResultVerifyToken);
            if (!requestResultVerifyToken.Succeeded)
                return Ok(requestResult);
            else
                user = requestResultVerifyToken.Model;

            var requestResultUser = await _iBusinessUsers.Validate(user, cancellationToken);

            requestResult.Add(requestResultUser);
            if (!requestResultUser.Succeeded)
                return Ok(requestResult);

            var requestResultRefreshToken = await _iBusinessRefreshTokens.Refresh(user.UserId, authentication.RefreshToken, cancellationToken);

            requestResult.Add(requestResultRefreshToken);
            if (!requestResultRefreshToken.Succeeded)
                return Ok(requestResult);

            var requestResultAuthentication = _iBusinessAuthentications.Create(requestResultRefreshToken.Model.Token, user);

            requestResult.Add(requestResultAuthentication);
            if (requestResultAuthentication.Succeeded)
                requestResult.Model = requestResultAuthentication.Model;

            return Ok(requestResult);
        }
    }
}