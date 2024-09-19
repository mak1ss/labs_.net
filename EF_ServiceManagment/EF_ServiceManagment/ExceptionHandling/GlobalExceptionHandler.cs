using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Oops! Internal server error occured"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
