using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public interface IProductCategoryRepository : IRepository<CommonDescription>
    {
        Task<IReadOnlyList<ProductCategoryDTOs>> GetAllProductCategories();
        Task<ProductCategoryDTOs> GetProductCategoriesById(long id);
        Task<string> UpdateProductCategory(CommonDescription entity, int productTypeId);
    }
}
