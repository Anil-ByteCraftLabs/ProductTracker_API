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
    [Authorize("Admin")]
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
            if (templateRequestDTOs.TempFormat.Count <=0)
                throw new Exception("Template format can not be blank.");
           if (templateRequestDTOs.OrgId <= 0)
                throw new Exception("Organization Id is not valid.");
           

            var template = new Template
            {
                OrgId = templateRequestDTOs.OrgId,
                TempFormat = JsonSerializer.Serialize(templateRequestDTOs.TempFormat),
                IsDefault = templateRequestDTOs.IsDefault,
                CreatedBy = templateRequestDTOs.CreatedBy
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

    }
}
