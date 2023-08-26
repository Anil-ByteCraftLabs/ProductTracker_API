using Newtonsoft.Json;
using ProductTracker.Api.Models;
using ProductTracker.Logging;
using System.Net;

namespace ProductTracker.Api.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)

            // Create a custom error response object
            var apiResponse = new ApiResponse<CustomException>();
            apiResponse.Success = false;
            //apiResponse.Message = "There are some technical error. Please try after some time";
            apiResponse.Message = exception.Message;

            // log the exception 
            Logger.Instance.Error("Exception:", exception);
            //var errorResponse = new
            //{
            //    message = "An error occurred while processing your request.",
            //    exception.Message
            //};

            // Serialize the error response object to JSON and write it to the response
            var json = JsonConvert.SerializeObject(apiResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
