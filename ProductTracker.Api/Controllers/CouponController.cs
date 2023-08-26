using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using System.Data.SqlClient;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        public async Task<ApiResponse<List<CouponsData>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<CouponsData>>();

            var data = await _unitOfWork.Coupons.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<CouponsData>> GetById(int id)
        {

            var apiResponse = new ApiResponse<CouponsData>();
            var data = await _unitOfWork.Coupons.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(CouponsData couponsData)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Coupons.AddAsync(couponsData);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(CouponsData couponsData)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Coupons.UpdateAsync(couponsData);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Coupons.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        #endregion



    }
}
