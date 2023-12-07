using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public interface IPlantRepository :  IRepository<Plant>
    {
        Task<IReadOnlyList<PlantDtos>> GetAllPlants();
        Task<PlantDtos> GetAllPlantById(long id);

        Task<IReadOnlyList<PlantDtos>> GetPlantsByUserId(string userId);

        Task<bool> CheckIfPlantCanBeDelete(long id);
    }
}
