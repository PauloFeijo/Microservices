using Microservice.Producer.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microservice.Producer.Api.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                return;
            }
            base.OnException(context);
        }
    }
}
