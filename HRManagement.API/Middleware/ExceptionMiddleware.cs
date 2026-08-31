using System.Text.Json;
using HRManagement.Core.Exceptions;
using HRManagement.Core.DTOs;




namespace HRManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex,"Validation error occurred.");

                await WriteResponse(context,400,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An unhandled exception occurred.");

                await WriteResponse(context,500,"Something went wrong.");
            }
        }

        private async Task WriteResponse(HttpContext context,int statusCode,string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponseDto
            {
                Success = false,
                Message = message,
                Errors = new List<string>()
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
    
    
