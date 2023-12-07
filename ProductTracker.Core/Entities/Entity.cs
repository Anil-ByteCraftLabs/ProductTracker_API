using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public string CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }

    }
}
