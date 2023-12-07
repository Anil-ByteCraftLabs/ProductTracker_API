using Microsoft.AspNetCore.Identity;

namespace ProductTracker.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public string? Role { get; set; }
        public bool IsActive { get; set; }
        //public bool? IsTeacher { get; set; }

        //public bool? IsStudent { get; set; }

        //public string SchoolId { get; set; }

        //public string? RoldId { get; set; }

        //public string Id { get; set; }
        //public override string UserName { get; set; }
        //public string NormalizedUserName { get; set; }
        //public string Email { get; set; }
        ////public string NormalizedEmail { get; set; }
        //public string PasswordHash { get; set; }

        // Additional properties can be added here if needed.
        public int OrganizationId { get; set; }
        public int PlantId { get; set; }

        public string? Role { get; set; }
    }
}
