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
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Sql.Queries;


namespace ProductTracker.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        // private readonly IConfiguration configuration;
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;

        public ProductRepository(DapperContext dapperContext, IUserRepository userRepository)
        {
            this._dapperContext = dapperContext;
            _userRepository = userRepository;   
        }

        public async Task<string> AddAsync(Product entity)
        {
            return await SaveProduct(entity);

        }

        public async Task<string> DeleteAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.ExecuteAsync(ProductDataQueries.DeleteProduct, new { ProductId = id });
                return result.ToString();
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                //connection.Open();
                var result = await connection.QueryAsync<Product>(ProductDataQueries.AllProduct);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<ProductResponseDTOs>> GetAllProducts()
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<ProductResponseDTOs>(ProductDataQueries.AllProduct, commandType: CommandType.StoredProcedure);
            //return result.ToList();
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

        public async Task<Product> GetByIdAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Product>(ProductDataQueries.ProductById, new { ProductId = id });
                return result;
            }
        }

        public async Task<ProductResponseDTOs> GetProductById(long id)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<ProductResponseDTOs>(ProductDataQueries.AllProduct, commandType: CommandType.StoredProcedure);
            var data = result.ToList().Where(p => p.Id == id).SingleOrDefault();
            data.CreatedByName = _userRepository.GetByIdAsync(data.CreatedBy).Result.UserName;
            if (!String.IsNullOrEmpty(data.UpdatedBy))
            {
                data.UpdatedByName = _userRepository.GetByIdAsync(data.UpdatedBy).Result.UserName;

            }
            return data;
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
            parameters.Add("Description", entity.Description);
            parameters.Add("Quantity", entity.Quantity);
            parameters.Add("FssaiCode", entity.FSSICode);
            parameters.Add("Category", entity.ProductCategory);
            parameters.Add("Weight", entity.ProductWeight);
            parameters.Add("Price", entity.Price);
            if (entity.PriceStartDate != null)
            {
                parameters.Add("PriceStartDate", entity.PriceStartDate);
            }
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(ProductDataQueries.SaveProduct, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();

        }

    }
}
