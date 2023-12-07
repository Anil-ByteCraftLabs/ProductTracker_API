using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;

namespace ProductTracker.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IReadOnlyList<UserResponseDTOs>> GetAllUsers();
        Task<UserResponseDTOs> GetAllUserById(long id);
        Task<UserResponseDTOs> GetByIdAsync(string id);
    }
}
