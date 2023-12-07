using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.DTO.Response
{
    public class TemplateResponseDTOs: BaseResponseDTO
    {
        public int TemplateId { get; set; }
        public int OrgId { get; set; }
        public string  OrgName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string TempFormat { get; set; }
        public List<TempFormat> TempFormatJson { get; set; }
    }
    public class TempFormat
    {
        public string Width { get; set; }
        public string Height { get; set; }
        public string MRP { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string QRCode { get; set; }
    }
}
