using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class BatchResponseDTOs : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? BatchName { get; set; }
        public string? BatchNo { get; set; }
        public int PlantId { get; set; }
        public string? PlantName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int NumberOfCoupon { get; set; }
        public int NoOfPrintedCoupons { get; set; }
        public int Status { get; set; }
    }
}
