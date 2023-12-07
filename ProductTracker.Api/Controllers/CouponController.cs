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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class CouponController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        public async Task<ApiResponse<List<CouponResponseDTO>>> GetAll(int batchId, string startDate, string endDate, bool isActive, int skipRecords, int takeRecords)
        {
            var apiResponse = new ApiResponse<List<CouponResponseDTO>>();
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime result;
                if (!DateTime.TryParse(startDate, out result))
                    throw new Exception("Please select a valid date start date.");
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime result;
                if (!DateTime.TryParse(startDate, out result))
                    throw new Exception("Please select a valid date end date.");
            }

            var data = await _unitOfWork.Coupons.GetBatchAllCoupons(batchId, startDate, endDate,isActive, skipRecords,takeRecords);
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<CouponResponseDTO>> GetById(int id)
        {

            var apiResponse = new ApiResponse<CouponResponseDTO>();
            var data = await _unitOfWork.Coupons.GetCouponDetails(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        //[HttpPost]
        //public async Task<ApiResponse<string>> Add(CouponsData couponsData)
        //{
        //    var apiResponse = new ApiResponse<string>();

        //    var data = await _unitOfWork.Coupons.AddAsync(couponsData);
        //    apiResponse.Success = true;
        //    apiResponse.Result = data;
        //    return apiResponse;
        //}

        [HttpPut]
        public async Task<ApiResponse<string>> GenerateCoupons(CouponRequestDTOs couponRequestDTOs)
        {
            if (couponRequestDTOs.BatchId <=0)
                throw new Exception("Please select a valid batch Id");
            if (string.IsNullOrEmpty(couponRequestDTOs.OrgAlias))
                throw new Exception("Org Alias can not be blank.");
            if (couponRequestDTOs.NoOfCoupons <= 0)
                throw new Exception("Number of coupons must be a positive number.");
            

            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Coupons.GenerateCoupons(couponRequestDTOs.BatchId, couponRequestDTOs.OrgAlias, couponRequestDTOs.NoOfCoupons, couponRequestDTOs.CreatedBy);
            if(data !=1)
                throw new Exception("Total nomber of coupens available for the selected batch is : "+ data.ToString());
            apiResponse.Success = true;
            apiResponse.Result = data.ToString();
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

        [HttpPut("UpdateCoupons")]
        public async Task<ApiResponse<string>> UpdateCoupons(CouponPutRequestDTOs couponPutRequestDTOs)
        {
            var data = await _unitOfWork.Coupons.UpdateCoupons(couponPutRequestDTOs);

            var apiResponse = new ApiResponse<string>();
            apiResponse.Success = true;
            apiResponse.Result = data.ToString();

            return apiResponse;
        }

            #endregion



        }
}
