using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalApi.Controllers.V1
{
    public class AuthenticationsController : BaseControllerV1
    {
        private readonly IBusinessAuthentications _iBAuthentications;

        public AuthenticationsController(IBusinessAuthentications iBAuthentications)
        {
            _iBAuthentications = iBAuthentications;
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Authenticate(User user, CancellationToken cancellationToken)
        {
            var requestResult = await _iBAuthentications.Authenticate(user, cancellationToken);

            return Ok(requestResult);
        }
    }
}