using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI.ExceptionHandling
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BadRequestExceptionHandler> _logger;

        public BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is not ValidationException validationException)
            {
                return false;
            }

            _logger.LogError(validationException, "Validation exception occured: {Message}", validationException.Message);

            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = validationException.Message
            };

            httpContext.Response.StatusCode = (int)problemDetails.Status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
