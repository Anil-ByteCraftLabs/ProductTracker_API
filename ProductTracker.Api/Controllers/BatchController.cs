//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Authorization;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using System.Data.SqlClient;

namespace ProductTracker.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BatchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [Authorize("Admin")]
        [HttpGet]
        public async Task<ApiResponse<List<BatchData>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<BatchData>>();

            var data = await _unitOfWork.Batches.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList(); 
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<BatchData>> GetById(int id)
        {

            var apiResponse = new ApiResponse<BatchData>();

            var data = await _unitOfWork.Batches.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(BatchData batchData)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Batches.AddAsync(batchData);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(BatchData batchData)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Batches.UpdateAsync(batchData);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Batches.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        #endregion


    }
}
