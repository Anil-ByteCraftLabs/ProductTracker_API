using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductTracker.Api.Authorization;
using ProductTracker.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _jwtUtils;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IJwtUtils jwtUtils)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
             var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                //var token = GenerateJwtToken(user, roles);
                var token = _jwtUtils.GenerateJwtToken(user, roles);

                return Ok(new { token });
            }

            return Unauthorized();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateUser(string username, string email, string password, string roleid, int CompanyId, int PlantId)
        {
            // Check if a user with the same username already exists
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            // Check if a user with the same email already exists
            existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return BadRequest("Email already exists.");
            }
            var roleName = await _roleManager.FindByIdAsync(roleid);
            // Create a new ApplicationUser object
            var newUser = new ApplicationUser
            {
                UserName = username,
                Email = email,
                CompanyId = CompanyId,
                PlantId = PlantId,
                IsActive =true
                
                // You can add additional properties to ApplicationUser here if needed.
            };

            // Create the user with the specified password
            var result = await _userManager.CreateAsync(newUser, password);
            var newUserDetails = _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);


            if (result.Succeeded)
            {
                var userId = newUser.Id;
                await AddUserToRole(userId, roleid);
                return Ok("User created successfully.");

            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private async Task<bool> AddUserToRole(string userId, string roldId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roleName = await _roleManager.FindByIdAsync(roldId);


            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName.Name);
                return result.Succeeded;
            }

            return false; // User not found
        }
        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("IsActive", user.IsActive.ToString())
            
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public int? ValidateJwtToken(string token)
        //{
        //    if (token == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);

        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        //        // return user id from JWT token if validation successful
        //        return userId;
        //    }
        //    catch
        //    {
        //        // return null if validation fails
        //        return null;
        //    }
        //}
    }
}
