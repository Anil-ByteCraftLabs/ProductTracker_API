using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Application.Interfaces;
using ProductTracker.Api.Authorization;
using ProductTracker.Api.Models;
using ProductTracker.Core.Entities;
using ProductTracker.Core.DTO.Request;
using Microsoft.IdentityModel.Tokens;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class ProductWeightController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductWeightController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        [HttpGet]
        public async Task<ApiResponse<List<CommonDescription>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<CommonDescription>>();

            var data = await _unitOfWork.ProductWeights.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }
        [HttpPost]
        public async Task<ApiResponse<string>> Add(CommonRequestDTOs commonRequestDTOs)
        {
            if (string.IsNullOrEmpty(commonRequestDTOs.Description))
                throw new Exception("Product weight description can not be blank.");


            var apiResponse = new ApiResponse<string>();
            var productWeight = new CommonDescription
            {
                Description = commonRequestDTOs.Description,
                CreatedBy = commonRequestDTOs.CreatedBy
            };

            var data = await _unitOfWork.ProductWeights.AddAsync(productWeight);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(CommonRequestDTOs commonRequestDTOs)
        {

            if (commonRequestDTOs.Id <= 0)
                throw new Exception("Product weight id must be a positive number.");
            if (string.IsNullOrEmpty(commonRequestDTOs.Description))
                throw new Exception("Product weight description can not be blank.");

            var apiResponse = new ApiResponse<string>();
            var productType = new CommonDescription
            {
                Id = commonRequestDTOs.Id,
                Description = commonRequestDTOs.Description,
                IsActive = commonRequestDTOs.IsActive,
                UpdatedBy = commonRequestDTOs.CreatedBy
            };
            var data = await _unitOfWork.ProductWeights.UpdateAsync(productType);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.ProductWeights.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse<CommonDescription>> GetById(int id)
        {

            var apiResponse = new ApiResponse<CommonDescription>();

            var data = await _unitOfWork.ProductWeights.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

    }
}
