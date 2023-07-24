using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class Product : Entity
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public int ProductWeight { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string FSSICode { get; set; }
        public decimal Price { get; set; }

    }
}
