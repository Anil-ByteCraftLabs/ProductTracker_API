using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class ProductRequestDTOs
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public string? FSSAICode { get; set; }
        public int Price { get; set; }
        public string? PriceStartdate { get; set; }
        public int ProductCategoryId { get; set; }
        public int WeightId { get; set; }
        public string CreatedBy { get; set; }
    }
}
