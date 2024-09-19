using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EF_ServiceManagment.WEBAPI.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState
                .SelectMany(message => message.Value.Errors)
                .Select(error => error.ErrorMessage)
                .ToList();

                throw new ValidationException(string.Join("; ", messages));
            }

            await next();
        }
    }
}
