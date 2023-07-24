using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class CouponsData: Entity
    {
        public int BatchId { get; set; }
        public string OrgAliasName { get; set; }
        public bool IsScanned { get; set; }
        public DateTime ScannedDate { get; set; }
        public int ScannedBy { get; set; }
        public string ScannedLocation { get; set; }
        public int ParentCouponId { get; set; }

        public string UniqueId { get; set; }

    }
}
