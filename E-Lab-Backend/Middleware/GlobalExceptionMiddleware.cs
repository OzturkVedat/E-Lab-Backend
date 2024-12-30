using E_Lab_Backend.Dto;

namespace E_Lab_Backend.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "A null reference exception occurred.");
                await HandleExceptionAsync(httpContext, "A required resource was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(httpContext, "An unexpected error occurred.");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var failureResult = new FailureResult(message)
            {
                Errors = new List<string> { message }
            };

            return context.Response.WriteAsJsonAsync(failureResult);
        }
    }
}
