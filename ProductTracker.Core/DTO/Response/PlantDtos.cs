using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class PlantDtos : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? PlantName { get; set; }
        public int OrgId { get; set; }
        public string? OrgName { get; set; }
       // public string? CreatedBy { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public DateTime? UpdatedOn { get; set; }
        //public string? UpdatedBy { get; set; }
        //public string? UpdatedByName { get; set; }
        //public string? CreatedByName { get; set; }
    }
}
