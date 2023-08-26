using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            // Check if a role with the same name already exists
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole != null)
            {
                return BadRequest("Role already exists.");
            }

            // Create a new IdentityRole object
            var newRole = new IdentityRole
            {
                Name = roleName
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
    }
}
