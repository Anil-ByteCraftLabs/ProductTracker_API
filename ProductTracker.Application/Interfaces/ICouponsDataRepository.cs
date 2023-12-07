using ProductTracker.Core.DTO.Request;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public interface ICouponsDataRepository : IRepository<CouponsData>
    {
        Task<int> GenerateCoupons(int batchId, string orgAlias, int noOfCoupons, string createdBy);
        Task<IReadOnlyList<CouponResponseDTO>> GetBatchAllCoupons(int BatchId, string startDate, string endDate, bool isActive, int skipRecords, int takeRecords);
        Task<CouponResponseDTO> GetCouponDetails(int CouponId);
        Task<int> UpdateCoupons(CouponPutRequestDTOs couponPutRequestDTOs);

    }
}
