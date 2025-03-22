using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class CouponsFilterRequestDTOs
    {
        public string UserId { get; set; }
        public int Orgid { get; set; }
        public String? StartDate { get; set; }
        public String? EndDate { get; set; }
    }
}
