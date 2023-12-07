using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class BatchData: Entity
    {
        public string Name { get; set; }
        public string BatchNo { get; set; }
        public int NoOfCoupons { get; set; }
        public int NoOfPrintedCoupons { get; set; }
        public int Status { get; set; }
        public int PlantId { get; set; }
        public int ProductId { get; set; }

    }
}
