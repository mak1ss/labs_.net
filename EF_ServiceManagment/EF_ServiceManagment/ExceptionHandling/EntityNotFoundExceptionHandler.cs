using EF_ServcieManagement.DAL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI.ExceptionHandling
{
    public class EntityNotFoundExceptionHandler : IExceptionHandler
    {

        private readonly ILogger<EntityNotFoundExceptionHandler> _logger;

        public EntityNotFoundExceptionHandler(ILogger<EntityNotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not EntityNotFoundException entityNotFoundException)
            {
                return false;
            }

            _logger.LogError(
                entityNotFoundException,
                "Entity not found: {Message}",
                entityNotFoundException.Message
                );

            var problemDetails = new ProblemDetails
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = entityNotFoundException.Message
            };

            httpContext.Response.StatusCode = (int)problemDetails.Status;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
