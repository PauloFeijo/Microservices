using FluentAssertions;
using Microservice.Producer.Api.Controllers;
using Microservice.Producer.Api.Models;
using Microservice.Producer.Api.Tests.Builders;
using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace Microservice.Producer.Api.Tests.Controllers
{
    public class EntryControllerTest
    {
        private readonly EntryController _entryController;
        private readonly MockRepository _mock;
        private readonly Mock<IEntryService> _entryService;
        private const string ExceptionMessage = "Error Message";

        public EntryControllerTest()
        {
            _mock = new MockRepository(MockBehavior.Strict);
            _entryService = _mock.Create<IEntryService>();
            _entryController = new EntryController(_entryService.Object);
        }

        [Fact]
        public void Post_ShouldExecuteCorrectly()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .Build();
            _entryService
                .Setup(x => x.PublishEntry(It.Is<Entry>(x => MathEntry(x, entryModelRequest))));

            var result = _entryController.Post(entryModelRequest);

            result.Should().BeOfType<OkObjectResult>();
            _mock.VerifyAll();
        }

        [Fact]
        public void Post_WhenServiceFail_ShouldThrowAnException()
        {
            var entryModelRequest = new EntryModelRequestBuilder()
                .WithValidValues()
                .Build();
            var exception = new Exception(ExceptionMessage);
            _entryService
                .Setup(x => x.PublishEntry(It.IsAny<Entry>()))
                .Throws(exception);

            Action func = () => _entryController.Post(entryModelRequest);

            func.Should().Throw<Exception>().WithMessage(ExceptionMessage);
            _mock.VerifyAll();
        }

        private static bool MathEntry(Entry entry, EntryModelRequest entryModelRequest) =>
            entry.UserName == entryModelRequest.UserName &&
            entry.Moment == entryModelRequest.Moment &&
            entry.Type == entryModelRequest.Type &&
            entry.Value == entryModelRequest.Value &&
            entry.AccountDescription == entryModelRequest.AccountDescription &&
            entry.Description == entryModelRequest.Description;
    }
}
