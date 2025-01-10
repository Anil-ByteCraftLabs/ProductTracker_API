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
    public class ProductType : IProductTypeRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        

        public ProductType(DapperContext dapperContext, IUserRepository userRepository, IOrganizationRepository oOrganizationRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
            _organizationRepository = oOrganizationRepository;
        }

        public async Task<string> AddAsync(CommonDescription entity)
        {
            return await SaveProductType(entity);
        }

        public async Task<string> DeleteAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                var result = await connection.ExecuteAsync(ProductTypeQueries.DeleteProductType, new { TypeIdId = id });
                return result.ToString();
            }
        }

        public async Task<IReadOnlyList<CommonDescription>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<CommonDescription>(ProductTypeQueries.AllProductType);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result.UserName;

                }
                if (data[i].Orgid>0)
                {
                    data[i].OrgName = GetOrgById(Convert.ToInt64(data[i].Orgid)).Result.OrgName;

                }
            }

            return data;

            
        }

        public async Task<CommonDescription> GetByIdAsync(long id)
        {
            using (var connection = _dapperContext.CreateManufacturerConnection())
            {
                // connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<CommonDescription>(ProductTypeQueries.ProductTypeById, new { TypeIdId = id });
                result.CreatedByName = _userRepository.GetByIdAsync(result.CreatedBy).Result.UserName;
                if (!String.IsNullOrEmpty(result.UpdatedBy))
                {
                    result.UpdatedByName = _userRepository.GetByIdAsync(result.UpdatedBy).Result.UserName;

                }
                if (result.Orgid > 0)
                {
                    result.OrgName = GetOrgById(Convert.ToInt64(result.Orgid)).Result.OrgName;

                }

                return result;
            }
        }

        public async Task<string> UpdateAsync(CommonDescription entity)
        {
            return await SaveProductType(entity);
        }

        private async Task<string> SaveProductType(CommonDescription entity)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductTypeId", entity.Id);
            parameters.Add("Description", entity.Description);
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("OrgId", entity.Orgid);
            parameters.Add("CreatedBy", entity.CreatedBy);
            parameters.Add("UpdatedBy", entity.UpdatedBy);

            var result = await connection.ExecuteAsync(ProductTypeQueries.SaveProductType, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();

        }

        private async Task<Organization> GetOrgById(long id)
        {
            return await _organizationRepository.GetByIdAsync(id);
        }
    }
}
