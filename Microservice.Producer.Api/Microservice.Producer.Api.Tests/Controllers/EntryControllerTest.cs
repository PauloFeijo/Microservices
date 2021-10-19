using FluentAssertions;
using Microservice.Producer.Api.Controllers;
using Microservice.Producer.Api.Tests.Builders;
using Microservice.Producer.Domain.Dtos;
using Microservice.Producer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
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
        public async Task Post_ShouldExecuteCorrectly()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .Build();
            _entryService
                .Setup(x => x.PublishEntry(It.Is<EntryDto>(x => x.Equals(entryDto))))
                .Returns(Task.CompletedTask);

            var result = await _entryController.Post(entryDto);

            result.Should().BeOfType<OkObjectResult>();
            _mock.VerifyAll();
        }

        [Fact]
        public async Task Post_WhenServiceFail_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .Build();
            var exception = new Exception(ExceptionMessage);
            _entryService
                .Setup(x => x.PublishEntry(It.IsAny<EntryDto>()))
                .Throws(exception);

            Func<Task> func = () => _entryController.Post(entryDto);

            await func.Should().ThrowAsync<Exception>().WithMessage(ExceptionMessage);
            _mock.VerifyAll();
        }
    }
}
