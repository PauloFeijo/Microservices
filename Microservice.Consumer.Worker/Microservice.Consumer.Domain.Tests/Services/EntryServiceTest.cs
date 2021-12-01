using AutoFixture;
using FluentAssertions;
using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.Repositories;
using Microservice.Consumer.Domain.Messages;
using Microservice.Consumer.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Consumer.Domain.Tests.Services
{
    public class EntryServiceTest
    {
        private const string ExceptionMessage = "Exception Message";
        private readonly EntryService _entryService;
        private readonly MockRepository _mock;
        private readonly Mock<IEntryRepository> _repository;
        private readonly Fixture _builder;

        public EntryServiceTest()
        {
            _builder = new Fixture();
            _mock = new MockRepository(MockBehavior.Strict);
            _repository = _mock.Create<IEntryRepository>();
            _entryService = new EntryService(_repository.Object);
        }

        [Fact]
        public async Task HandleMessage_ShouldExecuteCorrectly()
        {
            var message = _builder.Create<Message<Entry>>();
            _repository.Setup(x => x.Persist(It.Is<Entry>(x => x.Equals(message.Data))))
                .Returns(Task.CompletedTask);

            await _entryService.HandleMessage(message);

            _mock.VerifyAll();
        }

        [Fact]
        public async Task HandleMessage_WhenRepositoryFail_ShouldThrowAnException()
        {
            var exception = new Exception(ExceptionMessage);
            var message = _builder.Create<Message<Entry>>();
            _repository.Setup(x => x.Persist(It.IsAny<Entry>()))
                .Throws(exception);

            Func<Task> func = () => _entryService.HandleMessage(message);

            await func.Should().ThrowAsync<Exception>(ExceptionMessage);
            _mock.VerifyAll();
        }
    }
}
