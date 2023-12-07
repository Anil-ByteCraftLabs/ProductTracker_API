using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Api.Authorization;
using ProductTracker.Core.DTO.Request;
using Microsoft.IdentityModel.Tokens;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Admin")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ProductCategoryDTOs>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<ProductCategoryDTOs>>();

            var data = await _unitOfWork.ProductCategorys.GetAllProductCategories();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductCategoryDTOs>> GetById(int id)
        {

            var apiResponse = new ApiResponse<ProductCategoryDTOs>();

            var data = await _unitOfWork.ProductCategorys.GetProductCategoriesById(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(ProductCategoryRequestDTOs productCategoryRequestDTOs)
        {
            if (string.IsNullOrEmpty(productCategoryRequestDTOs.Name))
                throw new Exception("Plant category name can not be blank.");
            if (productCategoryRequestDTOs.ProductTypeId <= 0)
                throw new Exception("Product type id must be a positive number.");
            
            var apiResponse = new ApiResponse<string>();

            var category = new CommonDescription
            {
                Description= productCategoryRequestDTOs.Name,
                CreatedBy = productCategoryRequestDTOs.CreatedBy
            };
            var data = await _unitOfWork.ProductCategorys.UpdateProductCategory(category, productCategoryRequestDTOs.ProductTypeId);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }
        [HttpPut]
        public async Task<ApiResponse<string>> Update(ProductCategoryRequestDTOs productCategoryRequestDTOs)
        {
            if (string.IsNullOrEmpty(productCategoryRequestDTOs.Name))
                throw new Exception("Plant category name can not be blank.");
            if (productCategoryRequestDTOs.ProductTypeId <= 0)
                throw new Exception("Product type id must be a positive number.");

            var apiResponse = new ApiResponse<string>();

            var category = new CommonDescription
            {
                Id= productCategoryRequestDTOs.Id,
                Description = productCategoryRequestDTOs.Name,
                IsActive = productCategoryRequestDTOs.IsActive,
                UpdatedBy = productCategoryRequestDTOs.CreatedBy
            };
            var data = await _unitOfWork.ProductCategorys.UpdateProductCategory(category, productCategoryRequestDTOs.ProductTypeId);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }
        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.ProductCategorys.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

    }
}
