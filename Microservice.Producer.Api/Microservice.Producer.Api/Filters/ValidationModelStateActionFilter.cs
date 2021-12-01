using Microservice.Producer.Domain.Exceptions;
using Microservice.Producer.Infra.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microservice.Producer.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class ValidationModelStateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                var allErrorMessages = modelState
                    .Values
                    .SelectMany(value => value.Errors)
                    .Select(d => d.ErrorMessage)
                    .ToPipedMessage();

                throw new CustomException(allErrorMessages);
            }
        }
    }
}
