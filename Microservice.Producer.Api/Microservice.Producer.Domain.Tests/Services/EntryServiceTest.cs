using AutoFixture;
using FluentAssertions;
using Microservice.Producer.Domain.Dtos;
using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Interfaces;
using Microservice.Producer.Domain.Messages;
using Microservice.Producer.Domain.Services;
using Microservice.Producer.Domain.Tests.Builders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Producer.Domain.Tests.Services
{
    public class EntryServiceTest
    {
        private const string ExceptionMessage = "Exception Message";
        private readonly EntryService _entryService;
        private readonly MockRepository _mock;
        private readonly Mock<IMessageBroker> _messageBroker;
        private readonly Mock<ILogger<EntryService>> _logger;

        public EntryServiceTest()
        {
            _mock = new MockRepository(MockBehavior.Strict);
            _messageBroker = _mock.Create<IMessageBroker>();
            _logger = _mock.Create<ILogger<EntryService>>(MockBehavior.Loose);
            _entryService = new EntryService(_messageBroker.Object, _logger.Object);
        }

        [Fact]
        public async Task PublishEntry_ShouldExecuteCorrectly()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .Build();
            _messageBroker
                .Setup(x => x.Publish(It.Is<Message<Entry>>(x => MathEntry(x.Data, entryDto))))
                .Returns(Task.CompletedTask);

            await _entryService.PublishEntry(entryDto);

            _mock.VerifyAll();
        }

        [Fact]
        public async Task PublishEntry_WhenMessageBrokerFail_ShouldThrowAnException()
        {
            var entryDto = new EntryDtoBuilder()
                .WithValidValues()
                .Build();
            var exception = new Exception(ExceptionMessage);
            _messageBroker
                .Setup(x => x.Publish(It.IsAny<Message<Entry>>()))
                .Throws(exception);

            Func<Task> func = () => _entryService.PublishEntry(entryDto);

            await func.Should().ThrowAsync<Exception>(ExceptionMessage);
            _mock.VerifyAll();
        }

        private static bool MathEntry(Entry entry, EntryDto entryDto) => 
            entry.UserName == entryDto.UserName &&
            entry.Moment == entryDto.Moment &&
            entry.Type == entryDto.Type &&
            entry.Value == entryDto.Value &&
            entry.AccountDescription == entryDto.AccountDescription &&
            entry.Description == entryDto.Description;
    }
}
