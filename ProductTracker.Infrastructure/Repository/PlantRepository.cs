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
    public class PlantRepository : IPlantRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;

        public PlantRepository(DapperContext dapperContext, IUserRepository userRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;   
        }
        public Task<string> AddAsync(Plant entity)
        {
            return SavePlant(entity);
        }

        public async Task<string> DeleteAsync(long id)
        {
            if (!(await CheckIfPlantCanBeDelete(id)))
                return "This plant has batch attached so can't be deleted.";

            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.ExecuteAsync(PlantQueries.DeletePlant, new { PlantId = id });
            return result.ToString();
        }

        public async Task<IReadOnlyList<Plant>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<Plant>(PlantQueries.AllPlants);
            var user = _userRepository.GetByIdAsync("bc23e3ec-ca32-44fe-8ee1-871dbab45c02");
            return result.ToList();
        }

        public async Task<Plant> GetByIdAsync(long id)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QuerySingleOrDefaultAsync<Plant>(PlantQueries.PlantById, new { PlantId = id });
            return result;
        }

        public Task<string> UpdateAsync(Plant entity)
        {
            return SavePlant(entity);
        }

        public async Task<IReadOnlyList<PlantDtos>> GetAllPlants()
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<PlantDtos>(PlantQueries.AllPlants, commandType: CommandType.StoredProcedure);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result.UserName;

                }
            }
            
            return data;
        }

        private async Task<string> SavePlant(Plant entity)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var parameters = new DynamicParameters();
            parameters.Add("PlantId", entity.Id);
            parameters.Add("Name", entity.PlantName);
            parameters.Add("Location", entity.PlantLocation);
            parameters.Add("OrgId", entity.Orgid);
            parameters.Add("IsActive ", entity.IsActive);
            parameters.Add("CreatedBy", entity.CreatedBy);
            parameters.Add("UpdatedBy", entity.UpdatedBy);

            var result = await connection.ExecuteAsync(PlantQueries.SavePlant, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }

        public async Task<PlantDtos> GetAllPlantById(long id)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<PlantDtos>(PlantQueries.AllPlants, commandType: CommandType.StoredProcedure);
            var data =  result.ToList().Where(p => p.Id == id).SingleOrDefault();
            data.CreatedByName = _userRepository.GetByIdAsync(data.CreatedBy).Result?.UserName;
            if (!String.IsNullOrEmpty(data.UpdatedBy))
            {
                data.UpdatedByName = _userRepository.GetByIdAsync(data.UpdatedBy).Result.UserName;

            }

            return data;
        }

        public async Task<IReadOnlyList<PlantDtos>> GetPlantsByUserId(string userId)
        {
            var userOrg = _userRepository.GetByIdAsync(userId).Result.OrganizationId;
            
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<PlantDtos>(PlantQueries.AllPlants, commandType: CommandType.StoredProcedure);
            var data = result.Where(u => u.OrgId == userOrg).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result.UserName;

                }
            }

            return data;
        }

        public async Task<bool> CheckIfPlantCanBeDelete(long id)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("PlantId", id);
            parameters.Add("Result", DbType.Int32, direction: ParameterDirection.Output);
            await connection.ExecuteAsync(PlantQueries.CanPlantBeDeleted, parameters, commandType: CommandType.StoredProcedure);
            var result = parameters.Get<int>("@Result");
            return Convert.ToBoolean(result);

        }
    }
}
