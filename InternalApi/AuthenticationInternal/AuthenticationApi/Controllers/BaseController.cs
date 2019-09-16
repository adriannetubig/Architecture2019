using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [EnableCors("CORS")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize, ApiController]
    public class BaseController : ControllerBase
    {
        protected virtual string Username => User.Identities.FirstOrDefault().Name;
        protected virtual int UserId => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}
