using Dapper;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Infrastructure.Repository
{
    public class CouponsDataRepository : ICouponsDataRepository
    {
        private readonly DapperContext _dapperContext;

        public CouponsDataRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<string> AddAsync(CouponsData entity)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(CouponsDataQueries.AddCoupon, entity);
                return result.ToString();
            }
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CouponsData>> GetAllAsync()
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QueryAsync<CouponsData>(CouponsDataQueries.AllCoupons);
                return result.ToList();
            }
        }

        public async Task<CouponsData> GetByIdAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<CouponsData>(CouponsDataQueries.CouponById, new { CouponId = id });
                return result;
            }
        }

        public async Task<string> UpdateAsync(CouponsData entity)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(CouponsDataQueries.UpdateCoupon, entity);
                return result.ToString();
            }
        }
    }
}
