using ProductTracker.Api.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracker.Application.Interfaces;
using ProductTracker.Api.Models;
using ProductTracker.Core.DTO.Request;
using ProductTracker.Core.Entities;

using System.Text.Json;
using ProductTracker.Core.DTO.Response;

namespace ProductTracker.Api.Controllers
{
    [Authorize("Super Admin, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TemplateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Add(TemplateRequestDTOs templateRequestDTOs)
        {
            if ( String.IsNullOrEmpty( templateRequestDTOs.Name))
                throw new Exception("Template name can not be blank.");
            if (templateRequestDTOs.OrgId <= 0)
                throw new Exception("Organization Id is not valid.");

            var user = HttpContext.Items["User"] as dynamic;
            var template = new Template
            {
                OrgId = templateRequestDTOs.OrgId,
                IsDefault = templateRequestDTOs.IsDefault,
                IsActive = templateRequestDTOs.IsActive,
                Name = templateRequestDTOs.Name,
                CreatedBy = user?.Id
            };
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.TemplateRepositorys.AddAsync(template);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpGet("GetAllTemplate")]
        public async Task<ApiResponse<List<TemplateResponseDTOs>>> GetAllTemplate()
        {
            var apiResponse = new ApiResponse<List<TemplateResponseDTOs>>();

            var data = await _unitOfWork.TemplateRepositorys.GetAllTemplates();
            apiResponse.Success = true;
            apiResponse.Result = data.ToList();
            return apiResponse;
        }

        [HttpGet("{id}/format")]
        public async Task<ApiResponse<TempFormat>> GetById(int id)
        {

            var apiResponse = new ApiResponse<TempFormat>();

            var data = await _unitOfWork.TemplateRepositorys.GetTemplatesById(id);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }

        [HttpPost("{id}/format")]
        public async Task<ApiResponse<string>>  SaveFormat(int id, TempFormat tempFormat)
        {

           if (id <= 0)
                throw new Exception("Please select a valid template Id.");

            var user = HttpContext.Items["User"] as dynamic;
           
            var apiResponse = new ApiResponse<string>();

            var data = await _unitOfWork.TemplateRepositorys.SaveTemplateFormat(id, user?.Id, tempFormat);
            apiResponse.Success = true;
            apiResponse.Result = data;
            return apiResponse;
        }
    }



    
}
