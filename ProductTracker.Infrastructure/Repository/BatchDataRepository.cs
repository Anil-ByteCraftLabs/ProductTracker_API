using Dapper;
using ProductTracker.Application.Interfaces;
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
using static Dapper.SqlMapper;

namespace ProductTracker.Infrastructure.Repository
{
    public class BatchDataRepository : IBatchDataRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;
        private readonly IPlantRepository _plantRepository;

        public BatchDataRepository(DapperContext dapperContext, IUserRepository userRepository, IPlantRepository plantRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
            _plantRepository = plantRepository;
        }

        public async Task<string> AddAsync(BatchData entity)
        {
          
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", entity.Id);
            parameters.Add("Name", entity.Name);
            parameters.Add("BatchNo", entity.BatchNo);
            parameters.Add("NoOfCoupons", entity.NoOfCoupons);
            parameters.Add("PlantId", entity.PlantId);
            parameters.Add("ProductId", entity.ProductId);
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

        public async Task<IReadOnlyList<BatchResponseDTOs>> GetAllBatches()
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<BatchResponseDTOs>(BatchDataQueries.AllBatches, commandType: CommandType.StoredProcedure);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                data[i].PlantName = _plantRepository.GetAllPlantById(data[i].PlantId).Result?.PlantName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result?.UserName;

                }
            }

            return data;
        }

        public async Task<BatchResponseDTOs> GetBatchById(long id)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<BatchResponseDTOs>(BatchDataQueries.AllBatches, commandType: CommandType.StoredProcedure);
            var data = result.ToList().Where(p => p.Id == id).SingleOrDefault();
            data.CreatedByName = _userRepository.GetByIdAsync(data.CreatedBy).Result.UserName;
            data.PlantName = _plantRepository.GetAllPlantById(data.PlantId).Result?.PlantName;
            if (!String.IsNullOrEmpty(data.UpdatedBy))
            {
                data.UpdatedByName = _userRepository.GetByIdAsync(data.UpdatedBy).Result.UserName;

            }

            return data;
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

        public async Task<IReadOnlyList<BatchResponseDTOs>> GetUserBatches(string userId,string startDate, string endDate)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            parameters.Add("StartDate", startDate);
            parameters.Add("EndDate", endDate);

            var result = await connection.QueryAsync<BatchResponseDTOs>(BatchDataQueries.AllUserBatches, parameters, commandType: CommandType.StoredProcedure);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                data[i].PlantName = _plantRepository.GetAllPlantById(data[i].PlantId).Result?.PlantName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result?.UserName;

                }
            }

            return data;
        }

        public async Task<string> UpdateAsync(BatchData entity)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", entity.Id);
            parameters.Add("Name", entity.Name);
            parameters.Add("BatchNo", entity.BatchNo);
            parameters.Add("NoOfCoupons", entity.NoOfCoupons);
            parameters.Add("PlantId", entity.PlantId);
            parameters.Add("ProductId", entity.ProductId);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(BatchDataQueries.SaveBatchData, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }
    }
}
