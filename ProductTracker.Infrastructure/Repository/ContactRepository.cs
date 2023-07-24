using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Sql.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProductTracker.Infrastructure.Context;

namespace ProductTracker.Infrastructure.Repository
{
    public class ContactRepository : IContactRepository
    {
        #region ===[ Private Members ]=============================================================

        private readonly DapperContext _dapperContext;

        #endregion

        #region ===[ Constructor ]=================================================================

        public ContactRepository(DapperContext dapperContext)
        {
            this._dapperContext = dapperContext;
        }

        #endregion

        #region ===[ IContactRepository Methods ]==================================================

        public async Task<IReadOnlyList<Contact>> GetAllAsync()
        {
            //using (IDbConnection connection = new SqlConnection(_dapperContext.CreateConnection()))
            using (var connection = _dapperContext.CreateDefaultConnection())
            {
                //connection.Open();
                var result = await connection.QueryAsync<Contact>(ContactQueries.AllContact);
                return result.ToList();
            }
        }

        public async Task<Contact> GetByIdAsync(long id)
        {
            //using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            using (var connection = _dapperContext.CreateDefaultConnection())
            {
                //connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Contact>(ContactQueries.ContactById, new { ContactId = id });
                return result;
            }
        }

        public async Task<string> AddAsync(Contact entity)
        {
            //using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            using (var connection = _dapperContext.CreateDefaultConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(ContactQueries.AddContact, entity);
                return result.ToString();
            }
        }

        public async Task<string> UpdateAsync(Contact entity)
        {
            //using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            using (var connection = _dapperContext.CreateDefaultConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(ContactQueries.UpdateContact, entity);
                return result.ToString();
            }
        }

        public async Task<string> DeleteAsync(long id)
        {
            //using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            using (var connection = _dapperContext.CreateDefaultConnection())
            {
                var result = await connection.ExecuteAsync(OrganizationQueries.DeleteOrganization, new { OrgId = id });
                return result.ToString();
            }
        }

        #endregion
    }
}
