using Microsoft.AspNetCore.Mvc;

namespace ErrorLoggerApi.Controllers.V1
{
    [Route("api/v1/[controller]")]//ToDo: Use this if apiversioning is supported
    public abstract class BaseControllerV1 : BaseController
    {
    }
}
