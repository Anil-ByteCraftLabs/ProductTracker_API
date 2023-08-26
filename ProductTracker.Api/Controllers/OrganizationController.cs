using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using System.Data.SqlClient;
using ProductTracker.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ProductTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymousAttribute]
    public class OrganizationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ===[ Public Methods ]==============================================================

        [HttpGet]
        public async Task<ApiResponse<List<Organization>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<Organization>>();

            var data = await _unitOfWork.Organizations.GetAllAsync();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Organization>> GetById(int id)
        {

            var apiResponse = new ApiResponse<Organization>();

            var data = await _unitOfWork.Organizations.GetByIdAsync(id);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(Organization organization)
        {
            var requestFile = HttpContext.Request.Form.Files[0];
            if (requestFile.Length > 0)
            {
                organization.LogoFileName = requestFile.FileName;
                organization.Logo = requestFile.OpenReadStream();
            }
            var apiResponse = new ApiResponse<string>();


            var data = await _unitOfWork.Organizations.AddAsync(organization);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpPut]
        public async Task<ApiResponse<string>> Update(Organization organization)
        {
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.Organizations.UpdateAsync(organization);
            apiResponse.Success = true;
            apiResponse.Result = data;

            return apiResponse;
        }

        [HttpDelete]
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

        #endregion


    }
}
