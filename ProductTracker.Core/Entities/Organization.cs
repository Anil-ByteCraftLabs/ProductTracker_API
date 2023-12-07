using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class Organization: Entity
    {
        public string? OrgName { get; set; }
        public string? AliasName { get; set; }
        public string? DBPath { get; set; }
        [JsonIgnore]
        public DateTime? DeActivationDate { get; set; }
        public string? LogoFilePath { get; set; }
        public string? LogoFileName { get; set; }
    }
}
