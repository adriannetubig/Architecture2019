using AuthenticationApi.ApiModel;
using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using BaseModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationApi.Controllers.V1
{
    public class UsersController : BaseControllerV1
    {
        private readonly IBusinessRoles _iBusinessRoles;
        private readonly IBusinessUsers _iBusinessUsers;

        public UsersController(IBusinessRoles iBusinessRoles, IBusinessUsers iBusinessUsers)
        {
            _iBusinessRoles = iBusinessRoles;
            _iBusinessUsers = iBusinessUsers;
        }

        [HttpPut]
        public async Task<IActionResult> Create(User user, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessUsers.Create(user, cancellationToken);
            return Ok(requestResult);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Read(int userId, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<UserUpdate>()
            {
                Model = new UserUpdate()
            };

            var requestResultUser = await _iBusinessUsers.Read(userId, cancellationToken);
            requestResult.Add(requestResultUser);

            if (requestResultUser.Succeeded)
                requestResult.Model.User = requestResultUser.Model;

            var requestResultRole = await _iBusinessRoles.Read(cancellationToken);
            requestResult.Add(requestResultRole);

            if (requestResultRole.Succeeded)
                requestResult.Model.Roles = requestResultRole.Model;

            return Ok(requestResult);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessUsers.Update(user, cancellationToken);

            return Ok(requestResult);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Read(PageFilter pageFilter, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessUsers.Read(pageFilter, cancellationToken);

            return Ok(requestResult);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(User user, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessUsers.ChangePassword(user, cancellationToken);

            return Ok(requestResult);
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(User user, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessUsers.UpdatePassword(user, cancellationToken);
            return Ok(requestResult);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessUsers.Delete(userId, cancellationToken);
            return Ok(requestResult);
        }
    }
}