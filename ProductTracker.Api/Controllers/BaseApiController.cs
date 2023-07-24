using ProductTracker.Api.Filter;
using Microsoft.AspNetCore.Mvc;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    //[TypeFilter(typeof(AuthorizationFilterAttribute))]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}