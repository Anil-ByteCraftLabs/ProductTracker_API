﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductTracker.Api.Models;

namespace ProductTracker.Api.Authorization
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public AuthorizeAttribute(string role)  
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var user = (ApplicationUser)context.HttpContext.Items["User"];
            if (user == null || (user.Role.ToLower() != _role.ToLower()))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
