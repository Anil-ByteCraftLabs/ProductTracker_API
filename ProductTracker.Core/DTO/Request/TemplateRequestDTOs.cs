using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class TemplateRequestDTOs
    {
        public int OrgId { get; set; }
        public bool IsDefault { get; set; }
       public string? CreatedBy { get; set; }

        public List<TempFormat> TempFormat { get; set; }
    }

  
}
