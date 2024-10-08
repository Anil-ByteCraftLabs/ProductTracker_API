﻿using ProductTracker.Api.Models;
using ProductTracker.Application.Interfaces;
using ProductTracker.Core.Entities;
using ProductTracker.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ProductTracker.Api.Controllers
{
    public class UserController : BaseApiController
    {
        #region ===[ Private Members ]=============================================================

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region ===[ Constructor ]=================================================================

        /// <summary>
        /// Initialize ContactController by injecting an object type of IUnitOfWork
        /// </summary>
        public UserController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        //#region ===[ Public Methods ]==============================================================

        //[HttpGet]
        //public async Task<ApiResponse<List<User>>> GetAll()
        //{
        //    var apiResponse = new ApiResponse<List<User>>();

        //    var data = await _unitOfWork.Users.GetAllAsync();
        //    apiResponse.Success = true;
        //    apiResponse.Result = data.ToList();
        //    return apiResponse;
        //}

        //[HttpGet("{id}")]
        //public async Task<ApiResponse<User>> GetById(int id)
        //{

        //    var apiResponse = new ApiResponse<User>();

        //    var data = await _unitOfWork.Users.GetByIdAsync(id);
        //    apiResponse.Success = true;
        //    apiResponse.Result = data;
        //    return apiResponse;
        //}

        //[HttpPost]
        //public async Task<ApiResponse<string>> Add(User contact)
        //{
        //    var apiResponse = new ApiResponse<string>();

        //    var data = await _unitOfWork.Users.AddAsync(contact);
        //    apiResponse.Success = true;
        //    apiResponse.Result = data;
        //    return apiResponse;
        //}

        //[HttpPut]
        //public async Task<ApiResponse<string>> Update(User contact)
        //{
        //    var apiResponse = new ApiResponse<string>();

        //    var data = await _unitOfWork.Users.UpdateAsync(contact);
        //    apiResponse.Success = true;
        //    apiResponse.Result = data;
        //    return apiResponse;
        //}

        //[HttpDelete]
        //public async Task<ApiResponse<string>> Delete(int id)
        //{
        //    var apiResponse = new ApiResponse<string>();

        //    var data = await _unitOfWork.Users.DeleteAsync(id);
        //    apiResponse.Success = true;
        //    apiResponse.Result = data;
        //    return apiResponse;
        //}

        //#endregion
    }
}
