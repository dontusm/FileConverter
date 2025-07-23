using System.Net;
using System.Text.Json;

namespace FileConverter.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка: {Message}", ex.ToString());
                await HandleExceptionAsync(context, ex, cancellationToken);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = exception switch
            {
                NotSupportedException => (int)HttpStatusCode.BadRequest,        
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                statusCode = response.StatusCode
            });

            return response.WriteAsync(result, cancellationToken);
        }
    }
}