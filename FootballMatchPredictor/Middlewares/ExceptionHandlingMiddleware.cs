using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Result;
using System.Net;

namespace FootballMatchPredictor.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var errorMessage = exception.Message;
            var response = exception switch
            {
                UnauthorizedAccessException => new BaseResult() { ErrorMessage = errorMessage, ErrorCode = (int)StatusCode.Unauthorized },
                _ => new BaseResult() { ErrorMessage = errorMessage, ErrorCode = (int)HttpStatusCode.InternalServerError },
            };

            httpContext.Response.StatusCode = (int)response.ErrorCode;
            httpContext.Response.Redirect($"/home/error/{response.ErrorMessage}");

            return Task.CompletedTask;
        }
    }
}
