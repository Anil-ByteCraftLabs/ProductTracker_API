using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductTracker.Core.DTO.Response
{
    public class StateDTOs : BaseResponseDTO
     {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? Name { get; set; }
        public int?CountryId { get; set; }
    }
    public class DistrictDTOs : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }

        public string? StateName { get; set; }
    }
}
