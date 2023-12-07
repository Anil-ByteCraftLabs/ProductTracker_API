using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class UserResponseDTOs : BaseResponseDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public int OrganizationId { get; set; }
        public int PlantId { get; set; }
        public string? Organization { get; set; }
        public string? Plant { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }


    }
}
