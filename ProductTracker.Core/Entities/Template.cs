using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class Template: Entity
    {
        public int TemplateId { get; set; }
        public int OrgId { get; set; }
        public bool IsDefault { get; set; }
        public string? TempFormat { get; set; }
        
    }
}
