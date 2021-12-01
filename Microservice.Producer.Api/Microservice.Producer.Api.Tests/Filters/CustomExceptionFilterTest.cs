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
    public class CustomExceptionFilterTest
    {
        private readonly CustomExceptionFilter _filter;

        public CustomExceptionFilterTest()
        {
            _filter = new CustomExceptionFilter();
        }

        [Fact]
        public void OnException_whenIsValidationException_ShouldReturnBadRequest()
        {
            var errorMessage = "Error Message";
            var exception = new CustomException(errorMessage);
            var exceptionContext = CreateExceptionContext(exception);

            _filter.OnException(exceptionContext);

            exceptionContext.Result.Should().BeOfType<BadRequestObjectResult>();
            exceptionContext.Exception.Should().BeEquivalentTo(exception);
        }

        [Fact]
        public void OnException_whenIsNotValidationException_ShouldReturnNull()
        {
            var errorMessage = "Error Message";
            var exception = new Exception(errorMessage);
            var exceptionContext = CreateExceptionContext(exception);

            _filter.OnException(exceptionContext);

            exceptionContext.Result.Should().BeNull();
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
