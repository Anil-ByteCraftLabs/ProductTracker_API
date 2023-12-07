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
    public class TemplateRepository : ITemplateRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public TemplateRepository(DapperContext dapperContext, IUserRepository userRepository, IOrganizationRepository organizationRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<string> AddAsync(Template entity)
        {
            using var connection = _dapperContext.CreateManufacturerConnection();

            var parameters = new DynamicParameters();
            parameters.Add("OrgId", entity.OrgId);
            parameters.Add("IsDefault", entity.IsDefault);
            parameters.Add("TempFormat", entity.TempFormat);
            parameters.Add("CreatedBy", entity.CreatedBy);

            var result = await connection.ExecuteAsync(TemplateQueries.SaveTemplate, parameters, commandType: CommandType.StoredProcedure);
            return result.ToString();
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Template>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<TemplateResponseDTOs>> GetAllTemplates()
        {
            using var connection = _dapperContext.CreateManufacturerConnection();
            var result = await connection.QueryAsync<TemplateResponseDTOs>(TemplateQueries.AllTemplates, commandType: CommandType.StoredProcedure);
            var data = result.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].CreatedByName = _userRepository?.GetByIdAsync(data[i].CreatedBy).Result?.UserName;
                data[i].OrgName = _organizationRepository.GetByIdAsync(data[i].OrgId).Result?.OrgName;
                if (!String.IsNullOrEmpty(data[i].UpdatedBy))
                {
                    data[i].UpdatedByName = _userRepository.GetByIdAsync(data[i].UpdatedBy).Result?.UserName;

                }
            }
            return data;
        }

        public Task<Template> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(Template entity)
        {
            throw new NotImplementedException();
        }
    }
}
