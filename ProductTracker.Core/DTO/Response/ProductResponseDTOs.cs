using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class ProductResponseDTOs : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }

        public int Quantity { get; set; }
        public string? FSSAICode { get; set; }
        public int ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public int WeightId { get; set; }
        public string? WeightDesc { get; set; }
        public int Price { get; set; }
    }
}
