using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Interfaces.MessageBroker;
using Microservice.Producer.Domain.Interfaces.Services;
using Microservice.Producer.Domain.Messages;
using Microsoft.Extensions.Logging;

namespace Microservice.Producer.Domain.Services
{
    public class EntryService : IEntryService
    {
        private readonly IMessageBrokerService _messageBroker;
        private readonly ILogger _logger;

        public EntryService(
            IMessageBrokerService messageBroker,
            ILogger<EntryService> logger)
        {
            _messageBroker = messageBroker;
            _logger = logger;
        }
        public void PublishEntry(Entry entry)
        {
            _logger.LogInformation($"Call the message service");
            var message = new Message<Entry>(entry);
            _messageBroker.Publish(message);
        }
    }
}
