using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class CouponPutRequestDTOs
    {
        public List<int> CouponIds { get; set; }
        public bool IsActive { get; set; }
        public string UpdatedBy { get; set; }
    }
}
