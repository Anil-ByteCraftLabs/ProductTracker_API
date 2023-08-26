using Dapper;
using ProductTracker.Application.Interfaces;
using ProductTracker.Application.Interfaces.FileStorage;
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

        public OrganizationRepository(DapperContext dapperContext,IFileStorageProvider storageProvider)
        {
            _dapperContext = dapperContext;
            _storageProvider = storageProvider;
        }

        public async Task<string> AddAsync(Organization entity)
        {
            // _storageProvider.SaveFileAsync(importDataFile.FilePath, importDataFileModel.FileStream).Wait();
            _storageProvider?.SaveFileAsync(entity.LogoFileName, entity.Logo);

            using var connection = _dapperContext.CreateAdminConnection();
            var parameters = new DynamicParameters();
            parameters.Add("OrgId", entity.Id);
            parameters.Add("OrgName", entity.OrgName);
            parameters.Add("AliasName", entity.AliasName);
            parameters.Add("DBPath", entity.DBPath);
            parameters.Add("DeActivationDate", entity.DeActivationDate);
            parameters.Add("CreatedBy", entity.CreatedBy);

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
            return result.ToList();
        }

        public async Task<Organization> GetByIdAsync(long id)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QuerySingleOrDefaultAsync<Organization>(OrganizationQueries.OrganizationById, new { OrgId = id });
            return result;
        }
        public async Task<string> GetDataBase(string alias)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QuerySingleOrDefaultAsync<string>(OrganizationQueries.GetDataBase, new { AliasName = alias });
            return result;
        }


        public async Task<string> UpdateAsync(Organization entity)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var parameters = new DynamicParameters();
            parameters.Add("OrgId", entity.Id);
            parameters.Add("OrgName", entity.OrgName);
            parameters.Add("AliasName", entity.AliasName);
            parameters.Add("DBPath", entity.DBPath);
            parameters.Add("DeActivationDate", entity.DeActivationDate);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(OrganizationQueries.SaveOrganization, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }
    }
}
