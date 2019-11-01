using BaseApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [EnableCors("CORS")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public abstract class BaseController : BaseApiController
    {
    }
}
