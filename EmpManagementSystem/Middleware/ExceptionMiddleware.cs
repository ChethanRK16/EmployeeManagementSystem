using System.Net;
using System.Text.Json;

namespace EmpManagementSystem.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ErrorResponse
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An unexpected error occurred. Please try again later."
                };

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
