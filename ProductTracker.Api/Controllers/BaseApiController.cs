using ProductTracker.Api.Filter;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Api.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    //[TypeFilter(typeof(AuthorizationFilterAttribute))]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ApplicationUser? LoggedInUser
        {
            get
            {
                return (ApplicationUser)HttpContext?.Items["User"];
                //return HttpContext?.Session?.GetObject<ApplicationUser>("LoggedInUser");
            }
        }

        protected bool IsAuthenticated => LoggedInUser != null;



    }
}