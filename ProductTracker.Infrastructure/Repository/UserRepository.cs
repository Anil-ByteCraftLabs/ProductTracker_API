using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Sql.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Core.DTO.Response;

namespace ProductTracker.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        #region ===[ Private Members ]=============================================================

        private readonly DapperContext _dapperContext;

        #endregion

        #region ===[ Constructor ]=================================================================

        public UserRepository(DapperContext dapperContext)
        {
            this._dapperContext = dapperContext;
        }

        #endregion

        #region ===[ IContactRepository Methods ]==================================================

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var result = await connection.QueryAsync<User>(UserQueries.AllUsers);
            return result.ToList();
        }

        public async Task<User> GetByIdAsync(long id)
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserById, new { UserId = id });
            return result;
        }

        public async Task<string> AddAsync(User entity)
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var parameters = new DynamicParameters();
            parameters.Add("Id", 0);
            parameters.Add("FName", entity.FName);
            parameters.Add("MName", entity.MName);
            parameters.Add("LName", entity.LName);
            parameters.Add("Email", entity.Email);
            parameters.Add("Password", entity.Password);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(UserQueries.AddUser, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
           
        }

        public async Task<string> UpdateAsync(User entity)
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id);
            parameters.Add("FName", entity.FName);
            parameters.Add("MName", entity.MName);
            parameters.Add("LName", entity.LName);
            parameters.Add("Email", entity.Email);
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("Password", entity.Password);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(UserQueries.AddUser, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }

        public async Task<string> DeleteAsync(long id)
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var result = await connection.ExecuteAsync(UserQueries.DeleteUser, new { Id = id });
            return result.ToString();
        }

        public async Task<IReadOnlyList<UserResponseDTOs>> GetAllUsers()
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var result = await connection.QueryAsync<UserResponseDTOs>(UserQueries.AllUsers, commandType: CommandType.StoredProcedure);
            var data = result.ToList();
            connection.Close();
            using var adminConnection = _dapperContext.CreateAdminConnection();

            for (int i = 0; i < data.Count; i++)
            {
                var organization = await adminConnection.QuerySingleOrDefaultAsync<Organization>(OrganizationQueries.OrganizationById, new { OrgId = data[i].OrganizationId });
                data[i].Organization = organization.OrgName;

                var plant = await adminConnection.QuerySingleOrDefaultAsync<Plant>(PlantQueries.PlantById, new { PlantId = data[i].PlantId });
                data[i].Plant = plant.PlantName;

            }
            adminConnection.Close();


            return data;

        }

        public async Task<UserResponseDTOs> GetAllUserById(long id)
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var result = await connection.QueryAsync<UserResponseDTOs>(UserQueries.AllUsers, commandType: CommandType.StoredProcedure);
            var data = result.ToList().Where(u => u.Id == id.ToString()).SingleOrDefault();
            connection.Close();
            using var adminConnection = _dapperContext.CreateAdminConnection();

                var organization = await adminConnection.QuerySingleOrDefaultAsync<Organization>(OrganizationQueries.OrganizationById, new { OrgId = data.OrganizationId });
                data.Organization = organization.OrgName;

                var plant = await adminConnection.QuerySingleOrDefaultAsync<Plant>(PlantQueries.PlantById, new { PlantId = data.PlantId });
                data.Plant = plant.PlantName;

            adminConnection.Close();


            return data;
        }

        public async Task<UserResponseDTOs> GetByIdAsync(string id)
        {
            using var connection = _dapperContext.CreateDefaultConnection();
            var result = await connection.QuerySingleOrDefaultAsync<UserResponseDTOs>(UserQueries.UserById, new { UserId = id });
            return result;
        }

        #endregion
    }
}
