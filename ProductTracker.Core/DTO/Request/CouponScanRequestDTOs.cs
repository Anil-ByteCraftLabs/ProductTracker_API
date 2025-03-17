using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class CouponScanRequestDTOs
    {
        public string CouponCode{ get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ScannedBy { get; set; }

    }
}
