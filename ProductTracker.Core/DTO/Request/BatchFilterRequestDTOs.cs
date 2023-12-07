using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class BatchFilterRequestDTOs
    {
        public string UserId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
