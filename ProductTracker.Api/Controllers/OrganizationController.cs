using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using System.Data.SqlClient;
using ProductTracker.Logging;
using ProductTracker.Api.Authorization;
using ProductTracker.Core.DTO.Request;
using Microsoft.IdentityModel.Tokens;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        [Authorize("Super Admin")]
        public async Task<ApiResponse<List<Organization>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<Organization>>();
            var data = await _unitOfWork.Organizations.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}")]
        [Authorize("Super Admin")]
        public async Task<ApiResponse<Organization>> GetById(int id)
        {

            var apiResponse = new ApiResponse<Organization>();

            var data = await _unitOfWork.Organizations.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPost("/file")]
        public async Task<string> AddFile(IFormFile logo)
        {
            return "Test";
        }

            [HttpPost]
        [Authorize("Super Admin")]
        public async Task<ApiResponse<string>> Add(OrganizationRequestDTOs organizationRequestDTO)
        {
            if (string.IsNullOrEmpty(organizationRequestDTO.Name))
                throw new Exception("Organization name can not be blank.");
            if (string.IsNullOrEmpty(organizationRequestDTO.AliasName))
                throw new Exception("AliasName name can not be blank.");
            if (string.IsNullOrEmpty(organizationRequestDTO.DBPath))
                throw new Exception("DBPath name can not be blank.");
            if (!string.IsNullOrEmpty(organizationRequestDTO.DeactivationDate))
            {
                DateTime result;
                if (!DateTime.TryParse(organizationRequestDTO.DeactivationDate, out result))
                    throw new Exception("Please select a valid date as deactivation date.");
            }
            
            var apiResponse = new ApiResponse<string>();
            var organization = new Organization
            {
                OrgName = organizationRequestDTO.Name, 
                AliasName= organizationRequestDTO.AliasName,
                DBPath= organizationRequestDTO.DBPath,
                DeActivationDate= string.IsNullOrEmpty(organizationRequestDTO.DeactivationDate)? null: Convert.ToDateTime(organizationRequestDTO.DeactivationDate),
                CreatedBy= organizationRequestDTO.CreatedBy,
                CreatedOn = DateTime.Now,
                IsActive = true

                
            };

            var data = await _unitOfWork.Organizations.AddAsync(organization);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPut]
        [Authorize("Super Admin")]
        public async Task<ApiResponse<string>> Update(OrganizationRequestDTOs organizationRequestDTO)
        {
            if (organizationRequestDTO.Id <= 0)
                throw new Exception("Organization Id can not be blank.");
            if (string.IsNullOrEmpty(organizationRequestDTO.Name))
                throw new Exception("Organization name can not be blank.");
            if (string.IsNullOrEmpty(organizationRequestDTO.AliasName))
                throw new Exception("AliasName name can not be blank.");
            if (string.IsNullOrEmpty(organizationRequestDTO.DBPath))
                throw new Exception("DBPath name can not be blank.");

            var apiResponse = new ApiResponse<string>();
            var organization = new Organization
            {
                Id = organizationRequestDTO.Id,
                OrgName = organizationRequestDTO.Name,
                AliasName = organizationRequestDTO.Name,
                DBPath = organizationRequestDTO.DBPath,
                DeActivationDate = string.IsNullOrEmpty(organizationRequestDTO.DeactivationDate) ? null : Convert.ToDateTime(organizationRequestDTO.DeactivationDate),
                UpdatedBy = organizationRequestDTO.CreatedBy,
                IsActive = organizationRequestDTO.IsActive


            };
            var data = await _unitOfWork.Organizations.UpdateAsync(organization);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpDelete]
        [Authorize("Super Admin")]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Organizations.DeleteAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        //[HttpGet]
        //public async Task<ApiResponse<string>> DBPath(string alias)
        //{
        //    var apiResponse = new ApiResponse<string>();

        //    var data = await _unitOfWork.Organizations.GetDataBase(alias);
        //    apiResponse.Success = true;
        //    apiResponse.Result = data;
        //    return apiResponse;
        //}

        [HttpGet("{userId}/Org")]
        [Authorize("Admin")]
        public async Task<ApiResponse<List<Organization>>> GetOrgByUserId( string userId)
        {
            var apiResponse = new ApiResponse<List<Organization>>();
            var data = await _unitOfWork.Organizations.GetOrganizationByUserId(userId);
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        #endregion


    }
}
