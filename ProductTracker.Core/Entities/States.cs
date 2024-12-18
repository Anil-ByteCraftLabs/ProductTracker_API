using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class States : Entity
    {
        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public int? CountryId { get; set; }

    }

    public class District : Entity
    {
        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public int? CountryId { get; set; }
       
        public int? StateId { get; set; }

}
}
