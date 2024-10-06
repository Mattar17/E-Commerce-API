using System.Net;
using System.Text.Json;
using Talabat.APIS.G02.Errors;

namespace Talabat.APIS.G02.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var Response = _environment.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse(500);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response, options);

                context.Response.WriteAsync(JsonResponse);
            }
        }
    }
}
