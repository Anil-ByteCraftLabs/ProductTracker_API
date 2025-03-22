using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class ScanHistoryResponseDTO
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public string ScannedBy { get; set; }
        public DateTime ScannedOn { get; set; }
        public string OrgAlias { get; set; }
        public decimal Latitude {  get; set; }
        public decimal Longitude { get; set; }

    }
}
