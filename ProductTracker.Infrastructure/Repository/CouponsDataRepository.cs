using Dapper;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Data;
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
            return await SaveCoupon(entity);
        }

        public async Task<string> DeleteAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.QueryAsync<CouponsData>(CouponsDataQueries.DeleteCoupon);
                return result.ToString();
            }
        }

        public async Task<IReadOnlyList<CouponsData>> GetAllAsync()
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.QueryAsync<CouponsData>(CouponsDataQueries.AllCoupons);
                return result.ToList();
            }
        }

        public async Task<CouponsData> GetByIdAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CouponsData>(CouponsDataQueries.CouponById, new { CouponId = id });
                return result;
            }
        }

        public async Task<string> UpdateAsync(CouponsData entity)
        {
            return await SaveCoupon(entity);
        }

        private async Task<string> SaveCoupon(CouponsData entity)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CouponId", entity.Id);
            parameters.Add("BatchId", entity.BatchId);
            parameters.Add("OrgAlias", entity.OrgAliasName);
            parameters.Add("ParentCouponId", entity.ParentCouponId);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(CouponsDataQueries.SaveCoupon, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();

        }

    }
}
