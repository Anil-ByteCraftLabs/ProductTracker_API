using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Sql.Queries;


namespace ProductTracker.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        // private readonly IConfiguration configuration;
        private readonly DapperContext _dapperContext;

        public ProductRepository(DapperContext dapperContext)
        {
            this._dapperContext = dapperContext;
        }

        public async Task<string> AddAsync(Product entity)
        {
            return await SaveProduct(entity);

        }

        public async Task<string> DeleteAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.ExecuteAsync(ProductQueries.DeleteProduct, new { ProductId = id });
                return result.ToString();
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            //using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QueryAsync<Product>(ProductQueries.AllProduct);
                return result.ToList();
            }
        }
        public async Task<Product> GetByIdAsync(long id)
        {
            //using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            using (var connection = _dapperContext.CreateDefaultConnection())
            {
                // connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(ProductQueries.ProductById, new { ProductId = id });
                return result;
            }
        }

        public async Task<string> UpdateAsync(Product entity)
        {
            return await SaveProduct(entity);
        }

        private async Task<string> SaveProduct(Product entity)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", entity.Id);
            parameters.Add("Name", entity.ProductName);
            parameters.Add("Type", entity.ProductType);
            parameters.Add("Weight", entity.ProductWeight);
            parameters.Add("FssaiCode", entity.FSSICode);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(ProductDataQueries.SaveProduct, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();

        }

    }
}
