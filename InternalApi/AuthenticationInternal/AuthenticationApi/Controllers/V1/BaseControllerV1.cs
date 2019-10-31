using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers.V1
{
    [Route("api/v1/[controller]")]
    public abstract class BaseControllerV1 : BaseController
    {
    }
}