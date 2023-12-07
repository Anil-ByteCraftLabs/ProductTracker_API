//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductTracker.Api.Authorization;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.DTO.Request;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using System.Data.SqlClient;

namespace ProductTracker.Api.Controllers
{
    [Authorize("Admin")]
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

        [HttpGet]
        public async Task<ApiResponse<List<BatchResponseDTOs>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<BatchResponseDTOs>>();

            var data = await _unitOfWork.Batches.GetAllBatches();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList(); 
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<BatchResponseDTOs>> GetById(int id)
        {

            var apiResponse = new ApiResponse<BatchResponseDTOs>();

            var data = await _unitOfWork.Batches.GetBatchById(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(BatchRequestDTOs batchRequestDTOs)
        {
            if (string.IsNullOrEmpty(batchRequestDTOs.BatchName))
                throw new Exception("Batch name can not be blank.");
            if (string.IsNullOrEmpty(batchRequestDTOs.BatchNo))
                throw new Exception("Batch number can not be blank.");
            if(batchRequestDTOs.PlantId <= 0)
                throw new Exception("Plant Id is not valid.");
            if (batchRequestDTOs.ProductId <= 0)
                throw new Exception("Product Id is not valid.");
            if (batchRequestDTOs.NumberOfCoupon <= 0)
                throw new Exception("Number of Coupons must be a positive number.");

            var batchData = new BatchData
            {
                Name = batchRequestDTOs.BatchName,
                BatchNo = batchRequestDTOs.BatchNo,
                PlantId = batchRequestDTOs.PlantId,
                ProductId = batchRequestDTOs.ProductId,
                NoOfCoupons = batchRequestDTOs.NumberOfCoupon,
                CreatedBy  = batchRequestDTOs.CreatedBy
            };
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Batches.AddAsync(batchData);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        
        [HttpPut]
        public async Task<ApiResponse<string>> Update(BatchRequestDTOs batchRequestDTOs)
        {
            if(batchRequestDTOs.Id<= 0)
                throw new Exception("Batch Id is not valid");
            if (string.IsNullOrEmpty(batchRequestDTOs.BatchName))
                throw new Exception("Batch name can not be blank.");
            if (string.IsNullOrEmpty(batchRequestDTOs.BatchNo))
                throw new Exception("Batch number can not be blank.");
            if (batchRequestDTOs.PlantId <= 0)
                throw new Exception("Plant Id is not valid.");
            if (batchRequestDTOs.ProductId <= 0)
                throw new Exception("Product Id is not valid.");
            if (batchRequestDTOs.NumberOfCoupon <= 0)
                throw new Exception("Number of Coupons must be a positive number.");

            var batchData = new BatchData
            {
                Id= batchRequestDTOs.Id,
                Name = batchRequestDTOs.BatchName,
                BatchNo = batchRequestDTOs.BatchNo,
                PlantId = batchRequestDTOs.PlantId,
                ProductId = batchRequestDTOs.ProductId,
                NoOfCoupons = batchRequestDTOs.NumberOfCoupon,
                CreatedBy = batchRequestDTOs.CreatedBy
            };
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Batches.AddAsync(batchData);
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

        [HttpPost("GetBatchByUser")]
        public async Task<ApiResponse<List<BatchResponseDTOs>>> GetBatchByUser(BatchFilterRequestDTOs batchFilterRequestDTOs)
        {
            if (!string.IsNullOrEmpty(batchFilterRequestDTOs.StartDate))
            {
                DateTime result;
                if (!DateTime.TryParse(batchFilterRequestDTOs.StartDate, out result))
                    throw new Exception("Please select a valid date as start date.");
            }
            if (!string.IsNullOrEmpty(batchFilterRequestDTOs.EndDate))
            {
                DateTime result;
                if (!DateTime.TryParse(batchFilterRequestDTOs.EndDate, out result))
                    throw new Exception("Please select a valid date as end date.");
            }


            var apiResponse = new ApiResponse<List<BatchResponseDTOs>>();
            var data = await _unitOfWork.Batches.GetUserBatches(batchFilterRequestDTOs.UserId, batchFilterRequestDTOs.StartDate, batchFilterRequestDTOs.EndDate);
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }


        #endregion


    }
}
