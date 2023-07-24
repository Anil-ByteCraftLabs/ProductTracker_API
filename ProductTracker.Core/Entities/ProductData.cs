using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class ProductData
    {
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public int ProductWeight { get; set; }
        public string FssaiCode { get; set; }

        public List<ProductPrice> ProductPrices = new();

    }
}
