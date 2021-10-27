using AutoFixture;
using FluentAssertions;
using Microservice.Query.Api.Controllers;
using Microservice.Query.Domain.Dtos;
using Microservice.Query.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Query.Api.Tests.Controllers
{
    public class EntryControllerTest
    {
        private readonly Fixture _builder;
        private readonly EntryController _entryController;
        private readonly MockRepository _mock;
        private readonly Mock<IEntryRepository> _entryRepository;
        private const string ExceptionMessage = "Error Message";

        public EntryControllerTest()
        {
            _builder = new Fixture();
            _mock = new MockRepository(MockBehavior.Strict);
            _entryRepository = _mock.Create<IEntryRepository>();
            _entryController = new EntryController(_entryRepository.Object);
        }

        [Fact]
        public async Task GetEntries_ShouldReturnOKWithAListOfEntries()
        {
            var param = _builder.Create<EntryParamsDto>();
            var entries = _builder.CreateMany<EntryDto>();
            _entryRepository
                .Setup(x => x.GetEntries(param))
                .ReturnsAsync(entries);

            var result = await _entryController.GetEntries(param);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(entries);
            _mock.VerifyAll();
        }

        [Fact]
        public async Task GetEntries_WhenEntriesNotFound_ShouldReturnOKWithAEmptyListOfEntries()
        {
            var param = _builder.Create<EntryParamsDto>();
            var entries = Enumerable.Empty<EntryDto>();
            _entryRepository
                .Setup(x => x.GetEntries(param))
                .ReturnsAsync(entries);

            var result = await _entryController.GetEntries(param);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(entries);
            _mock.VerifyAll();
        }

        [Fact]
        public async Task GetEntries_WhenRepositoryFail_ShouldThrowAnException()
        {
            var param = _builder.Create<EntryParamsDto>();
            var exception = new Exception(ExceptionMessage);
            _entryRepository
                .Setup(x => x.GetEntries(param))
                .Throws(exception);

            Func<Task> func = () => _entryController.GetEntries(param);

            await func.Should().ThrowAsync<Exception>().WithMessage(ExceptionMessage);
            _mock.VerifyAll();
        }

        [Fact]
        public async Task GetEntry_ShouldReturnOKWithOneEntry()
        {
            var id = _builder.Create<Guid>();
            var entry = _builder.Create<EntryDto>();
            _entryRepository
                .Setup(x => x.GetEntry(id))
                .ReturnsAsync(entry);

            var result = await _entryController.GetEntry(id);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(entry);
            _mock.VerifyAll();
        }

        [Fact]
        public async Task GetEntry_WhenEntryNotFound_ShouldReturnNoContent()
        {
            var id = _builder.Create<Guid>();
            var entry = (EntryDto)null;
            _entryRepository
                .Setup(x => x.GetEntry(id))
                .ReturnsAsync(entry);

            var result = await _entryController.GetEntry(id);

            result.Should().BeOfType<NoContentResult>();
            _mock.VerifyAll();
        }

        [Fact]
        public async Task GetEntry_WhenRepositoryFail_ShouldThrowAnException()
        {
            var id = _builder.Create<Guid>();
            var exception = new Exception(ExceptionMessage);
            _entryRepository
                .Setup(x => x.GetEntry(id))
                .Throws(exception);

            Func<Task> func = () => _entryController.GetEntry(id);

            await func.Should().ThrowAsync<Exception>().WithMessage(ExceptionMessage);
            _mock.VerifyAll();
        }
    }
}
