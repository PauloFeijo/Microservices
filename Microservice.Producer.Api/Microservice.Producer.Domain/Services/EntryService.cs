using Microservice.Producer.Domain.Dtos;
using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Interfaces;
using Microservice.Producer.Domain.Messages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Microservice.Producer.Domain.Services
{
    public class EntryService : IEntryService
    {
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger _logger;

        public EntryService(
            IMessageBroker messageBroker,
            ILogger<EntryService> logger)
        {
            _messageBroker = messageBroker;
            _logger = logger;
        }
        public async Task PublishEntry(EntryDto entryDto)
        {
            var entry = new Entry(entryDto);
            _logger.LogInformation($"Call the message service for the entry {entry.Id}");
            var message = new Message<Entry>(entry);
            await _messageBroker.Publish(message);
        }
    }
}
