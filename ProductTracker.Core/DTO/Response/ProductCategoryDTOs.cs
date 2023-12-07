using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class ProductCategoryDTOs : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public int ProductTypeId { get; set; }
        public string? ProductTypeName { get; set; }
    }
}
