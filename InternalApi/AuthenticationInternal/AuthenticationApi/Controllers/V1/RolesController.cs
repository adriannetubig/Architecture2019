using AuthenticationBusiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationApi.Controllers.V1
{
    public class RolesController : BaseControllerV1
    {
        private readonly IBusinessRoles _iBusinessRoles;

        public RolesController(IBusinessRoles iBusinessRoles)
        {
            _iBusinessRoles = iBusinessRoles;
        }

        [HttpGet]
        public async Task<IActionResult> Read(CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessRoles.Read(cancellationToken);

            return Ok(requestResult);
        }

    }
}