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
    public class PlantRepository : IPlantRepository
    {
        private readonly DapperContext _dapperContext;

        public PlantRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public Task<string> AddAsync(Plant entity)
        {
            return SavePlant(entity);
        }

        public async Task<string> DeleteAsync(long id)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.ExecuteAsync(PlantQueries.DeletePlant, new { PlantId = id });
            return result.ToString();
        }

        public async Task<IReadOnlyList<Plant>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<Plant>(PlantQueries.AllPlants);
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
    }
}
