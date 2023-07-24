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
    public class BatchDataRepository : IBatchDataRepository
    {
        private readonly DapperContext _dapperContext;

        public BatchDataRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<string> AddAsync(BatchData entity)
        {
          
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", entity.Id);
            parameters.Add("Name", entity.Name);
            parameters.Add("BatchNo", entity.BatchNo);
            parameters.Add("NoOfCoupons", entity.NoOfCoupons);
            parameters.Add("OrgId", entity.OrgId);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(BatchDataQueries.SaveBatchData, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();

        }

        public async Task<string> DeleteAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QueryAsync<BatchData>(BatchDataQueries.DeleteBatch,new { BatchId = id });
                return result.ToString();
            }
        }

        public async Task<IReadOnlyList<BatchData>> GetAllAsync()
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QueryAsync<BatchData>(BatchDataQueries.AllBatches);
                return result.ToList();
            }
        }

        public async Task<BatchData> GetByIdAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<BatchData>(BatchDataQueries.BatchById, new { OrgId = id });
                return result;
            }
        }

        public async Task<string> UpdateAsync(BatchData entity)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", entity.Id);
            parameters.Add("Name", entity.Name);
            parameters.Add("BatchNo", entity.BatchNo);
            parameters.Add("NoOfCoupons", entity.NoOfCoupons);
            parameters.Add("OrgId", entity.OrgId);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(BatchDataQueries.SaveBatchData, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }
    }
}
