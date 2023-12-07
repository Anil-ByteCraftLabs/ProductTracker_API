using ProductTracker.Application.Interfaces;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Core.Entities;

namespace ProductTracker.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyList<ProductResponseDTOs>> GetAllProducts();
        Task<ProductResponseDTOs> GetProductById(long id);
        //Task<string> UpdateProductCategory(CommonDescription entity, int productTypeId);
    }
}
