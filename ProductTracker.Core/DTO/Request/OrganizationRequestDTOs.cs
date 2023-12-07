using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductTracker.Core.DTO.Request
{
    public class OrganizationRequestDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AliasName { get; set; }
        public string DBPath { get; set; }
        public bool IsActive { get; set; }
        public string? DeactivationDate { get; set; }
        public string? CreatedBy { get; set; }


    }
}
