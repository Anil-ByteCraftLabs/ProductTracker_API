
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Core.Entities;
using ProductTracker.Api.Authorization;
using ProductTracker.Core.DTO.Request;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Super Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CommonRequestDTOs commonRequestDTOs)
        {
            if (string.IsNullOrEmpty(commonRequestDTOs.Description))
                throw new Exception("Role name can not be blank.");
            // Check if a role with the same name already exists
            var existingRole = await _roleManager.FindByNameAsync(commonRequestDTOs.Description);
            if (existingRole != null)
            {
                return BadRequest("Role already exists.");
            }

            // Create a new IdentityRole object
            var newRole = new IdentityRole
            {
                Name = commonRequestDTOs.Description
            };

            // Create the role
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        public async Task<ApiResponse<List<Microsoft.AspNetCore.Identity.IdentityRole>>> ListRoles()
        {
            var apiResponse = new ApiResponse<List<Microsoft.AspNetCore.Identity.IdentityRole>>();

          
            var roles =  _roleManager.Roles;
            apiResponse.Success = true;
            apiResponse.Result = roles.Where(r => r.NormalizedName != "SUPER ADMIN").ToList();
            return apiResponse;
        }
    }
}
