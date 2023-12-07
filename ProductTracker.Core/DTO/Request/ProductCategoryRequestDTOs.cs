using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Request
{
    public class ProductCategoryRequestDTOs
    {
        // string categoryName, int productTypeId, string createdBy

        public int Id { get; set; }
        public string?   Name { get; set; }
        public int ProductTypeId { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
