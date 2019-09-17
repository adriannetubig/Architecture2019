using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalApi.Controllers.V1
{
    public class UsersController : BaseControllerV1
    {
        private readonly IBusinessUsers _iBUsers;

        public UsersController(IBusinessUsers iBUsers)
        {
            _iBUsers = iBUsers;
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> Authenticate(User user, CancellationToken cancellationToken)
        {
            var requestResult = await _iBUsers.ChangePassword(user, cancellationToken);

            return Ok(requestResult);
        }
    }
}