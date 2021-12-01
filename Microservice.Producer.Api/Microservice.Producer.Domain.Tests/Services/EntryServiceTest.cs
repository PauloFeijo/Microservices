using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Interfaces.MessageBroker;
using Microservice.Producer.Domain.Messages;
using Microservice.Producer.Domain.Services;
using Microservice.Producer.Domain.Tests.Builders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Microservice.Producer.Domain.Tests.Services
{
    public class EntryServiceTest
    {
        private const string ExceptionMessage = "Exception Message";
        private readonly EntryService _entryService;
        private readonly MockRepository _mock;
        private readonly Mock<IMessageBrokerService> _messageBroker;
        private readonly Mock<ILogger<EntryService>> _logger;
        private readonly CompareLogic _compare;

        public EntryServiceTest()
        {
            _mock = new MockRepository(MockBehavior.Strict);
            _messageBroker = _mock.Create<IMessageBrokerService>();
            _logger = _mock.Create<ILogger<EntryService>>(MockBehavior.Loose);
            _entryService = new EntryService(_messageBroker.Object, _logger.Object);
            _compare = new CompareLogic();
        }

        [Fact]
        public void PublishEntry_ShouldExecuteCorrectly()
        {
            var entry = new EntryBuilder()
                .WithValidValues()
                .Build();
            _messageBroker
                .Setup(x => x.Publish(It.Is<Message<Entry>>(x => MathEntry(x.Data, entry))));

            _entryService.PublishEntry(entry);

            _mock.VerifyAll();
        }

        [Fact]
        public void PublishEntry_WhenMessageBrokerFail_ShouldThrowAnException()
        {
            var entry = new EntryBuilder()
                .WithValidValues()
                .Build();
            var exception = new Exception(ExceptionMessage);
            _messageBroker
                .Setup(x => x.Publish(It.IsAny<Message<Entry>>()))
                .Throws(exception);

            Action func = () => _entryService.PublishEntry(entry);

            func.Should().Throw<Exception>(ExceptionMessage);
            _mock.VerifyAll();
        }

        private bool MathEntry(Entry entry, Entry entryExpected) =>
            _compare.Compare(entryExpected, entry).AreEqual;
    }
}
