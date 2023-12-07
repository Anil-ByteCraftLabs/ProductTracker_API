using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public interface IBatchDataRepository : IRepository<BatchData>
    {
        Task<IReadOnlyList<BatchResponseDTOs>> GetAllBatches();
        Task<BatchResponseDTOs> GetBatchById(long id);
        Task<IReadOnlyList<BatchResponseDTOs>> GetUserBatches(string userId,string startDate, string endDate);

    }
}
