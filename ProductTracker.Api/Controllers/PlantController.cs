using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using System.Data.SqlClient;
using ProductTracker.Api.Authorization;
using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.DTO.Request;

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
        [Authorize("Super Admin")]
        public async Task<ApiResponse<List<PlantDtos>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<PlantDtos>>();

            var data = await _unitOfWork.Plants.GetAllPlants();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }

        [Authorize("Super Admin")]
        [HttpGet("{id}")]
        public async Task<ApiResponse<PlantDtos>> GetById(int id)
        {

            var apiResponse = new ApiResponse<PlantDtos>();

            var data = await _unitOfWork.Plants.GetAllPlantById(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [Authorize("Super Admin")]
        [HttpPost]
        public async Task<ApiResponse<string>> Add(PlantRequestDTOs plantRequestDTOs)
        {
            if (string.IsNullOrEmpty(plantRequestDTOs.PlantName))
                throw new Exception("Plant name can not be blank.");
            if (string.IsNullOrEmpty(plantRequestDTOs.Location))
                throw new Exception("Plant must have a location.");
            if (plantRequestDTOs.OrgId <= 0)
                throw new Exception("Please select an organization for plant.");
            var apiResponse = new ApiResponse<string>();

            var plant = new Plant
            {
                PlantName = plantRequestDTOs.PlantName,   
                PlantLocation = plantRequestDTOs.Location,
                Orgid = plantRequestDTOs.OrgId,
                CreatedBy = plantRequestDTOs.CreatedBy
            };
            var data = await _unitOfWork.Plants.AddAsync(plant);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [Authorize("Super Admin")]
        [HttpPut]
        public async Task<ApiResponse<string>> Update(PlantRequestDTOs plantRequestDTOs)
        {
            if (plantRequestDTOs.Id <= 0)
                throw new Exception("Plant Id must be a positive number.");
            if (string.IsNullOrEmpty(plantRequestDTOs.PlantName))
                throw new Exception("Plant name can not be blank.");
            if (string.IsNullOrEmpty(plantRequestDTOs.Location))
                throw new Exception("Plant must have a location.");
            if (plantRequestDTOs.OrgId <= 0)
                throw new Exception("Please select an organization for plant.");

            var apiResponse = new ApiResponse<string>();

            var plant = new Plant
            {
                Id = plantRequestDTOs.Id,
                PlantName = plantRequestDTOs.PlantName,
                PlantLocation = plantRequestDTOs.Location,
                Orgid = plantRequestDTOs.OrgId,
                UpdatedBy = plantRequestDTOs.CreatedBy,
                IsActive= plantRequestDTOs.IsActive
            };
            var data = await _unitOfWork.Plants.UpdateAsync(plant);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [Authorize("Super Admin")]
        [HttpDelete]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Plants.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [Authorize("Admin")]
        [HttpGet("{userId}/org")]
        public async Task<ApiResponse<List<PlantDtos>>> GetPlantByUser(string userId)
        {
            var apiResponse = new ApiResponse<List<PlantDtos>>();

            var data = await _unitOfWork.Plants.GetPlantsByUserId(userId);
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();

            return apiResponse;
        }

        #endregion



    }
}
