using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        public async Task<ApiResponse<List<Product>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<Product>>();

            var data = await _unitOfWork.Products.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Product>> GetById(int id)
        {

            var apiResponse = new ApiResponse<Product>();

            var data = await _unitOfWork.Products.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(Product product)
        {
            var apiResponse = new ApiResponse<string>();

                var data = await _unitOfWork.Products.AddAsync(product);
                apiResponse.Success = true;
                apiResponse.Result = data;
            
            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(Product product)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Products.UpdateAsync(product);
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
