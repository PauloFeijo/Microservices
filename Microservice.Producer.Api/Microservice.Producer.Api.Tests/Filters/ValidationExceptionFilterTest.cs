using FluentAssertions;
using Microservice.Producer.Api.Filters;
using Microservice.Producer.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using Xunit;

namespace Microservice.Producer.Api.Tests.Filters
{
    public class ValidationExceptionFilterTest
    {
        private readonly ValidationExceptionFilter _filter;

        public ValidationExceptionFilterTest()
        {
            _filter = new ValidationExceptionFilter();
        }

        [Fact]
        public void OnException_whenIsValidationException_ShouldReturnBadRequest()
        {
            var errorMessage = "Error Message";
            var exception = new ValidationException(errorMessage);
            var exceptionContext = CreateExceptionContext(exception);

            _filter.OnException(exceptionContext);

            exceptionContext.Result.Should().BeOfType<BadRequestObjectResult>();
            exceptionContext.Exception.Should().BeEquivalentTo(exception);
        }

        private ExceptionContext CreateExceptionContext(Exception exception)
        {
            var httpContext = new DefaultHttpContext();
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);
            var filters = new List<IFilterMetadata>() { _filter };
            return new ExceptionContext(actionContext, filters) { Exception = exception };
        }
    }
}
