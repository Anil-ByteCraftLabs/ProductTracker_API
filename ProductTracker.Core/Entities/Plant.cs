using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class Plant : Entity
    {
        public string? PlantName { get; set; }
        public string? PlantLocation { get; set; }
        public int Orgid { get; set; }
        public string? OrgName { get; set; }


    }
}
