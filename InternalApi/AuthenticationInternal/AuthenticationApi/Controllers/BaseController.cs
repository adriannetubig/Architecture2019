using BaseApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [EnableCors("CORS")]
    [Authorize]
    public abstract class BaseController : BaseApiController
    {
    }
}
