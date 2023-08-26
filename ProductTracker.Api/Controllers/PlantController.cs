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
    public class PlantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        public async Task<ApiResponse<List<Plant>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<Plant>>();

            var data = await _unitOfWork.Plants.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Plant>> GetById(int id)
        {

            var apiResponse = new ApiResponse<Plant>();

            var data = await _unitOfWork.Plants.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(Plant plant)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Plants.AddAsync(plant);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(Plant plant)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Plants.UpdateAsync(plant);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Plants.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        #endregion



    }
}
