using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Core.DTO.Response;

using ProductTracker.Api.Authorization;
using ProductTracker.Application.Interfaces;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("States")]
        [Authorize("Super Admin")]
        public async Task<ApiResponse<List<StateDTOs>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<StateDTOs>>();

            var data = await _unitOfWork.States.GetAllStates();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }

        [Authorize("Super Admin")]
        [HttpGet("{id}")]
        public async Task<ApiResponse<List<DistrictDTOs>>> GetDistricts(int id)
        {
            var apiResponse = new ApiResponse<List<DistrictDTOs>>();

            var data = await _unitOfWork.StateDistricts.GetStateDistricts(id);
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }
    }
}
