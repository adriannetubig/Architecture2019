using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers
{
    [EnableCors("CORS")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected virtual string Username => User.Identities.FirstOrDefault().Name;
        protected virtual int UserId => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}
