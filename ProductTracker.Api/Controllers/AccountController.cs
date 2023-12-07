using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductTracker.Api.Authorization;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.DTO.Request;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Super Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _jwtUtils;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IJwtUtils jwtUtils, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtUtils = jwtUtils;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymousAttribute]
        [HttpPost("login")]
        public async Task<ApiResponse<string>> Login(LoginViewModel model)
        {
            var apiResponse = new ApiResponse<string>();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                apiResponse.Success = false;
                apiResponse.Message = "Invalid login ID or password";
                return apiResponse;
            }


            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtUtils.GenerateJwtToken(user, roles);

            apiResponse.Success = true;
            apiResponse.Message = "Login Successed.";
            apiResponse.Result = token;
            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> CreateUser(UserRequestDTOs userRequestDTOs)
        {
            // Validations 
            if(string.IsNullOrEmpty(userRequestDTOs.Username))
                throw new Exception("Please provide a valid user name.");
            if (!IsValidEmail(userRequestDTOs.Email))
                throw new Exception("Please provide a valid user email.");
            if (string.IsNullOrEmpty(userRequestDTOs.Password))
                throw new Exception("Please provide a valid password.");
            if (string.IsNullOrEmpty(userRequestDTOs.Roleid))
                throw new Exception("Please select a valid role.");
            if (userRequestDTOs.OrganizationId<=0)
                throw new Exception("Please select a valid organization.");
            if (userRequestDTOs.PlantId <= 0)
                throw new Exception("Please select a valid plant.");
            if (!IsValidPassword(userRequestDTOs.Password))
                throw new Exception("Please provide a valid password.");


            
            var apiResponse = new ApiResponse<string>();

            // Check if a user with the same username already exists
            var existingUser = await _userManager.FindByNameAsync(userRequestDTOs.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            // Check if a user with the same email already exists
            existingUser = await _userManager.FindByEmailAsync(userRequestDTOs.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists.");
            }
            var roleName = await _roleManager.FindByIdAsync(userRequestDTOs.Roleid);
            // Create a new ApplicationUser object
            var newUser = new ApplicationUser
            {
                UserName = userRequestDTOs.Username,
                Email = userRequestDTOs.Email,
                OrganizationId = userRequestDTOs.OrganizationId,
                PlantId = userRequestDTOs.PlantId,
                IsActive = true

                // You can add additional properties to ApplicationUser here if needed.
            };

            // Create the user with the specified password
            var result = await _userManager.CreateAsync(newUser, userRequestDTOs.Password);
            var newUserDetails = _userManager.Users.FirstOrDefaultAsync(u => u.Email == userRequestDTOs.Email);


            if (result.Succeeded)
            {
                var userId = newUser.Id;
                await AddUserToRole(userId, userRequestDTOs.Roleid);
                apiResponse.Success = true;
                apiResponse.Message = "User created successfully.";
                
                return apiResponse;

            }
            else
            {
                throw new Exception(result.Errors.ToString());
               // return BadRequest(result.Errors);
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
        private bool IsValidEmail(string email)
        {
            // Regular expression for a simple email format validation
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Use Regex.IsMatch to check if the email matches the pattern
            return Regex.IsMatch(email, emailPattern);
        }

        private  bool IsValidPassword(string password)
        {
            // Check for at least one non-alphanumeric character
            if (!Regex.IsMatch(password, @"[^\w\d]"))
                return false;

            // Check for at least one digit
            if (!Regex.IsMatch(password, @"\d"))
                return false;

            // Check for at least one uppercase letter
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            // All requirements met
            return true;
        }


        [HttpGet]
        public async Task<ApiResponse<List<UserResponseDTOs>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<UserResponseDTOs>>();

            var data = await _unitOfWork.Users.GetAllUsers();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;

        }

    }
}
