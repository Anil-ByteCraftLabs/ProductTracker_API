using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public interface IStatesRepository : IRepository<States>
    {
        public  Task<IReadOnlyList<StateDTOs>> GetAllStates();
    }

    public interface IDistrictRepository : IRepository<District>
    {
        public Task<IReadOnlyList<DistrictDTOs>> GetStateDistricts( int stateId);
    }

    public interface ICityRepository : IRepository<City>
    {
        public Task<IReadOnlyList<CityDTOs>> GetStateCities(int stateId);
    }
}
