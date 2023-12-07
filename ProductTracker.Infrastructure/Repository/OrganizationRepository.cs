using Dapper;
using ProductTracker.Application.Interfaces;
using ProductTracker.Application.Interfaces.FileStorage;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Sql;
using ProductTracker.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Infrastructure.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IFileStorageProvider _storageProvider;
        private readonly IUserRepository _userRepository;

        public OrganizationRepository(DapperContext dapperContext,IFileStorageProvider storageProvider, IUserRepository userRepository)
        {
            _dapperContext = dapperContext;
            _storageProvider = storageProvider;
            _userRepository = userRepository;   
        }

        public async Task<string> AddAsync(Organization entity)
        {
            // _storageProvider.SaveFileAsync(importDataFile.FilePath, importDataFileModel.FileStream).Wait();
            //_storageProvider?.SaveFileAsync(entity.LogoFileName, entity.Logo);

            using var connection = _dapperContext.CreateAdminConnection();
            var parameters = new DynamicParameters();
            parameters.Add("OrgId", entity.Id);
            parameters.Add("OrgName", entity.OrgName);
            parameters.Add("AliasName", entity.AliasName);
            parameters.Add("DBPath", entity.DBPath);
            parameters.Add("DeActivationDate", entity.DeActivationDate);
            parameters.Add("CreatedBy", entity.CreatedBy);
            parameters.Add("UpdatedBy", entity.UpdatedBy);

            var result = await connection.ExecuteAsync(OrganizationQueries.SaveOrganization, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();

        }

        public async Task<string> DeleteAsync(long id)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.ExecuteAsync(OrganizationQueries.DeleteOrganization, new { OrgId = id });
            return result.ToString();
        }

        public async Task<IReadOnlyList<Organization>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<Organization>(OrganizationQueries.AllOrganization);
            var data =  result.ToList();
            connection.Close();
            using var defaultConnection = _dapperContext.CreateDefaultConnection();
            var allUser = await defaultConnection.QueryAsync<UserResponseDTOs>(UserQueries.AllUsers, commandType: CommandType.StoredProcedure);
            for (int i = 0; i < data.Count; i++)
            {
               
                data[i].CreatedByName = allUser.ToList().Where(u => u.Id == data[i].CreatedBy).SingleOrDefault().UserName;

                if(!string.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = allUser.ToList().Where(u => u.Id == data[i].UpdatedBy).SingleOrDefault().UserName;

                }
            }
            return data;
        }

        public async Task<Organization> GetByIdAsync(long id)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var data = await connection.QuerySingleOrDefaultAsync<Organization>(OrganizationQueries.OrganizationById, new { OrgId = id });
            connection.Close();
            using var defaultConnection = _dapperContext.CreateDefaultConnection();
            var allUser = await defaultConnection.QueryAsync<UserResponseDTOs>(UserQueries.AllUsers, commandType: CommandType.StoredProcedure);
            data.CreatedByName = allUser.ToList().Where(u => u.Id == data.CreatedBy).SingleOrDefault().UserName;

            if (!string.IsNullOrEmpty(data.UpdatedBy))
            {
                data.UpdatedByName = allUser.ToList().Where(u => u.Id == data.UpdatedBy).SingleOrDefault().UserName;

            }
            return data;
        }
        public async Task<string> GetDataBase(string alias)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QuerySingleOrDefaultAsync<string>(OrganizationQueries.GetDataBase, new { AliasName = alias });
            return result;
        }

        public async Task<IReadOnlyList<Organization>> GetOrganizationByUserId(string userId)
        {
            using var defaultConnection = _dapperContext.CreateDefaultConnection();
            var allUser = await defaultConnection.QueryAsync<UserResponseDTOs>(UserQueries.AllUsers, commandType: CommandType.StoredProcedure);
            var selectedUser = allUser.ToList().Where(u => u.Id == userId).SingleOrDefault();
           
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<Organization>(OrganizationQueries.AllOrganization);
            var data = result.Where(o => o.Id == selectedUser.OrganizationId).ToList();
            connection.Close();
          
            for (int i = 0; i < data.Count; i++)
            {

                data[i].CreatedByName = allUser.ToList().Where(u => u.Id == data[i].CreatedBy).SingleOrDefault().UserName;

                if (!string.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = allUser.ToList().Where(u => u.Id == data[i].UpdatedBy).SingleOrDefault().UserName;

                }
            }
            return data;

        }

        public async Task<string> UpdateAsync(Organization entity)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var parameters = new DynamicParameters();
            parameters.Add("OrgId", entity.Id);
            parameters.Add("OrgName", entity.OrgName);
            parameters.Add("AliasName", entity.AliasName);
            parameters.Add("DBPath", entity.DBPath);
            // new SqlParameter("visitDate", (object)visitDate ?? DBNull.Value),
            if (entity.DeActivationDate!= null )
            {
                parameters.Add("DeActivationDate", entity.DeActivationDate);
            }
            
            parameters.Add("CreatedBy", entity.CreatedBy);
            parameters.Add("UpdatedBy", entity.UpdatedBy);

            var result = await connection.ExecuteAsync(OrganizationQueries.SaveOrganization, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }
    }
}
