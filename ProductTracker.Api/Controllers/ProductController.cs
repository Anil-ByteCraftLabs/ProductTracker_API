using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ProductTracker.Api.Authorization;
using ProductTracker.Core.DTO.Request;
using ProductTracker.Core.DTO.Response;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize("Admin")]
    public class ProductController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        public async Task<ApiResponse<List<ProductResponseDTOs>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<ProductResponseDTOs>>();

            var data = await _unitOfWork.Products.GetAllProducts();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductResponseDTOs>> GetById(int id)
        {

            var apiResponse = new ApiResponse<ProductResponseDTOs>();

            var data = await _unitOfWork.Products.GetProductById(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(ProductRequestDTOs productDTOs)
        {
            if (string.IsNullOrEmpty(productDTOs.ProductName))
                throw new Exception("Product name can not be blank.");
            if (string.IsNullOrEmpty(productDTOs.FSSAICode))
                throw new Exception("Product should have a valid FSSAI code.");
            if (productDTOs.Quantity <= 0)
                throw new Exception("Quantity must be a positive number.");
            if (productDTOs.ProductCategoryId <= 0)
                throw new Exception("Please select a valid product category.");
            if (productDTOs.WeightId <= 0)
                throw new Exception("Please select a valid weight ID.");
            if (productDTOs.Price <= 0)
                throw new Exception("Price must be a positive number.");
            if (!string.IsNullOrEmpty(productDTOs.PriceStartdate))
            {
                DateTime result;
                if (!DateTime.TryParse(productDTOs.PriceStartdate, out result))
                    throw new Exception("Please select a valid price start date.");
            }

            var apiResponse = new ApiResponse<string>();
            var product = new Product
            {
                ProductName=productDTOs.ProductName,
                Description=productDTOs.Description,
                Quantity=productDTOs.Quantity,
                FSSICode=productDTOs.FSSAICode,
                ProductCategory = productDTOs.ProductCategoryId,
                ProductWeight = productDTOs.WeightId,
                Price=productDTOs.Price,
                PriceStartDate= string.IsNullOrEmpty(productDTOs.PriceStartdate) ? null : Convert.ToDateTime(productDTOs.PriceStartdate),
                CreatedBy = productDTOs.CreatedBy,
            };
                var data = await _unitOfWork.Products.AddAsync(product);
                apiResponse.Success = true;
                apiResponse.Result = data;
            
            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(ProductRequestDTOs productDTOs)
        {
            if (string.IsNullOrEmpty(productDTOs.ProductName))
                throw new Exception("Product name can not be blank.");
            if (string.IsNullOrEmpty(productDTOs.FSSAICode))
                throw new Exception("Product should have a valid FSST code.");
            if (productDTOs.Quantity <= 0)
                throw new Exception("Quantity must be a positive number.");
            if (productDTOs.ProductCategoryId <= 0)
                throw new Exception("Please select a valid product category.");
            if (productDTOs.WeightId <= 0)
                throw new Exception("Please select a valid weight Id.");
            if (productDTOs.Price <= 0)
                throw new Exception("Price must be a positive number.");
            if (productDTOs.Id<= 0)
                throw new Exception("Please select a valid product Id.");

            if (!string.IsNullOrEmpty(productDTOs.PriceStartdate))
            {
                DateTime result;
                if (!DateTime.TryParse(productDTOs.PriceStartdate, out result))
                    throw new Exception("Please select a valid price start date.");
            }

            var apiResponse = new ApiResponse<string>();
            var product = new Product
            {
                Id= productDTOs.Id,
                ProductName = productDTOs.ProductName,
                Description = productDTOs.Description,
                Quantity = productDTOs.Quantity,
                FSSICode = productDTOs.FSSAICode,
                ProductCategory = productDTOs.ProductCategoryId,
                ProductWeight = productDTOs.WeightId,
                Price = productDTOs.Price,
                PriceStartDate = string.IsNullOrEmpty(productDTOs.PriceStartdate) ? null : Convert.ToDateTime(productDTOs.PriceStartdate),
                CreatedBy = productDTOs.CreatedBy,
            };
            var data = await _unitOfWork.Products.AddAsync(product);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Products.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        #endregion

    }
}
