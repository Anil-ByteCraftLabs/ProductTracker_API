using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class PlantRequestDTOs
    {
        //string name, string location, int orgId, string createdBy

        public int Id { get; set; }
        public string PlantName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public int OrgId { get; set; }
        public string CreatedBy { get; set; }
    }
}
