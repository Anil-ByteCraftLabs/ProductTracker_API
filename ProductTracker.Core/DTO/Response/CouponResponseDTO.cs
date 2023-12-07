using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class CouponResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public int BatchId { get; set; }
        public string OrgAliasName { get; set; }
        public int ParentCouponId { get; set; }
        public bool IsScanned { get; set; }
        public DateTime? ScannedDate { get; set; }
        public bool IsActive { get; set; }

    }
}
