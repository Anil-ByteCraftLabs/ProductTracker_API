using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class CouponRequestDTOs 
    {
        // int batchId, string orgAlias, int noOfCoupons, string createdBy

        public int Id { get; set; }
        public int BatchId { get; set; }
        public string OrgAlias { get; set; }
        public int NoOfCoupons { get; set; }
        public string CreatedBy { get; set; }
    }
}
