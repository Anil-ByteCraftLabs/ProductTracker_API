using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class BatchRequestDTOs
    {
        // string name, string batchNo, int plantId, int productId, int numberOfCoupon, string createdBy
        public int Id { get; set; }
        public string BatchName { get; set; }
        public string BatchNo { get; set; }
        public int PlantId { get; set; }
        public int ProductId { get; set; }
        public int NumberOfCoupon { get; set; }
        public string CreatedBy { get; set; }

    }
}
