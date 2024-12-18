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
    public class StatesRepository : IStatesRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;

        public StatesRepository(DapperContext dapperContext, IUserRepository userRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
        }
        public Task<string> AddAsync(States entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<States>> GetAllAsync()
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<States>(StatesQueries.AllStates);
            var user = _userRepository.GetByIdAsync("bc23e3ec-ca32-44fe-8ee1-871dbab45c02");
            return result.ToList();
        }

        public async Task<IReadOnlyList<StateDTOs>> GetAllStates()
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var result = await connection.QueryAsync<StateDTOs>(StatesQueries.AllStates, commandType: CommandType.StoredProcedure);
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

        public Task<States> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(States entity)
        {
            throw new NotImplementedException();
        }
    }
    public class DistrictRepository : IDistrictRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IUserRepository _userRepository;

        public DistrictRepository(DapperContext dapperContext, IUserRepository userRepository)
        {
            _dapperContext = dapperContext;
            _userRepository = userRepository;
        }

        public Task<string> AddAsync(District entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<District>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<District> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<DistrictDTOs>> GetStateDistricts(int stateId)
        {
            using var connection = _dapperContext.CreateAdminConnection();
            var parameters = new DynamicParameters();
            parameters.Add("StateId", stateId);
            var result = await connection.QueryAsync<DistrictDTOs>(StatesQueries.StatesDistrict, parameters, commandType: CommandType.StoredProcedure);
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

        public Task<string> UpdateAsync(District entity)
        {
            throw new NotImplementedException();
        }
    }
}
