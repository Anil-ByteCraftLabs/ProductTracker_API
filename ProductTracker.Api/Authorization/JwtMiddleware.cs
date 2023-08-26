using Microsoft.AspNetCore.Identity;
using ProductTracker.Api.Models;

namespace ProductTracker.Api.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly UserManager<ApplicationUser> _userManager;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
            // _userManager = userManager;
            //_userManager = new UserManager<ApplicationUser> ();

        }
        public async Task Invoke(HttpContext context,IJwtUtils jwtUtils, UserManager<ApplicationUser> _userManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
               
                // attach user to context on successful jwt validation
                var user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                user.Role = roles.FirstOrDefault();


                context.Items["User"] = user;
            }

            await _next(context);
        }
    }
}
