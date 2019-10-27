using BaseApi;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLoggerApi.Controllers
{
    [EnableCors("CORS")]
    //[Route("api/v{v:apiVersion}/[controller]")]//ToDo: Use this if apiversioning is supported    
    public abstract class BaseController : BaseApiController
    {
    }
}
