using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class UserRequestDTOs
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Roleid { get; set; }
        public int OrganizationId { get; set; }
        public int PlantId { get; set; }


    }
}
