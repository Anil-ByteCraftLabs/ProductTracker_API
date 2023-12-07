using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Sql.Queries;

namespace ProductTracker.Infrastructure.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;

        public ProductCategoryRepository(DapperContext dapperContext, IUserRepository userRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
        }
        public Task<string> AddAsync(CommonDescription entity)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteAsync(long id)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.ExecuteAsync(ProductCategoryQueries.DeleteProductCategory, new { Id = id });
            return result.ToString();
        }

        public async Task<IReadOnlyList<CommonDescription>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<CommonDescription>(ProductCategoryQueries.AllProductCategory);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result?.UserName;

                }
            }

            return data;
        }

        public async Task<ProductCategoryDTOs> GetProductCategoriesById(long id)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<ProductCategoryDTOs>(ProductCategoryQueries.AllProductCategory, commandType: CommandType.StoredProcedure);
            var data = result.ToList().Where(p => p.Id == id).SingleOrDefault();
            data.CreatedByName = _userRepository.GetByIdAsync(data.CreatedBy).Result.UserName;
            if (!String.IsNullOrEmpty(data.UpdatedBy))
            {
                data.UpdatedByName = _userRepository.GetByIdAsync(data.UpdatedBy).Result.UserName;

            }

            return data;
        }

        public async Task<IReadOnlyList<ProductCategoryDTOs>> GetAllProductCategories()
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<ProductCategoryDTOs>(ProductCategoryQueries.AllProductCategory, commandType: CommandType.StoredProcedure);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result?.UserName;

                }
            }

            return data;
        }

        public Task<CommonDescription> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(CommonDescription entity)
        {
            throw new NotImplementedException();
        }

        private async Task<string> SaveProductCategory(CommonDescription entity, int productTypeId)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductCatId", entity.Id);
            parameters.Add("Name", entity.Description);
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("ProductTypeId", productTypeId);
            parameters.Add("CreatedBy", entity.CreatedBy);
            parameters.Add("UpdatedBy", entity.UpdatedBy);

            var result = await connection.ExecuteAsync(ProductCategoryQueries.SaveProductCategory, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }

        public async Task<string> UpdateProductCategory(CommonDescription entity, int productTypeId)
        {
            return await SaveProductCategory(entity, productTypeId);
        }
    }
}
