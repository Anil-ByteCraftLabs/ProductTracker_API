using Dapper;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.DTO.Request;
using ProductTracker.Core.DTO.Response;
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
        private readonly IUserRepository _userRepository;

        public CouponsDataRepository(DapperContext dapperContext, IUserRepository userRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
        }
        public async Task<string> AddAsync(CouponsData entity)
        {
            return await SaveCoupon(entity);
        }

        public async Task<string> DeleteAsync(long id)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.ExecuteAsync(CouponsDataQueries.DeleteCoupon, new { CouponId = id });
            return result.ToString();

        }

        public async Task<int> GenerateCoupons(int batchId, string orgAlias, int noOfCoupons, string createdBy)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("BatchId", batchId);
            parameters.Add("NoOfCoupons", noOfCoupons);
            parameters.Add("CreatedBy", createdBy);
            parameters.Add("OrgAlias", orgAlias);
            parameters.Add("Result", DbType.Int32 , direction: ParameterDirection.Output);

            await connection.ExecuteAsync(CouponsDataQueries.GenerateCoupons, parameters, commandType: CommandType.StoredProcedure);

            var result =  parameters.Get<int>("@Result");


            return result;
        }

        public async Task<IReadOnlyList<CouponsData>> GetAllAsync()
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.QueryAsync<CouponsData>(CouponsDataQueries.AllCoupons);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<CouponResponseDTO>> GetBatchAllCoupons(int BatchId, string startDate, string endDate, bool isActive, int skipRecords, int takeRecords)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("BatchId", BatchId);
            parameters.Add("StartDate", startDate);
            parameters.Add("EndDate", endDate);
            parameters.Add("Status", isActive);

            parameters.Add("skipRecords", skipRecords);
            parameters.Add("takeRecords", takeRecords);

            var result = await connection.QueryAsync<CouponResponseDTO>(CouponsDataQueries.BatchAllCoupons, parameters, commandType: CommandType.StoredProcedure);
           
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result?.UserName;

                }
            }

            return data;
        }

        public async Task<CouponsData> GetByIdAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CouponsData>(CouponsDataQueries.CouponById, new { CouponId = id });
                return result;
            }
        }

        public async Task<CouponResponseDTO> GetCouponDetails(int CouponId)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CouponId", CouponId);

            var result = await connection.QueryAsync<CouponResponseDTO>(CouponsDataQueries.CouponById, parameters, commandType: CommandType.StoredProcedure);

            var data = result.SingleOrDefault();
          
                data.CreatedByName = _userRepository.GetByIdAsync(data.CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data.UpdatedBy))
                {
                    data.UpdatedByName = _userRepository.GetByIdAsync(data.UpdatedBy).Result?.UserName;

                }
           

            return data;
        }

        public async Task<string> UpdateAsync(CouponsData entity)
        {
            return await SaveCoupon(entity);
        }

        public async Task<int> UpdateCoupons(CouponPutRequestDTOs couponPutRequestDTOs)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CouponIds", string.Join(", ", couponPutRequestDTOs.CouponIds));
            parameters.Add("IsActive", couponPutRequestDTOs.IsActive);
            parameters.Add("UpdatedBy", couponPutRequestDTOs.UpdatedBy);
            parameters.Add("Result", DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(CouponsDataQueries.UpdateCoupons, parameters, commandType: CommandType.StoredProcedure);

            var result = parameters.Get<int>("@Result");


            return result;
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
