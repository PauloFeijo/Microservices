using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces;
using Microservice.Consumer.Domain.Messages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repo;
        private readonly ILogger<EntryService> _logger;

        public EntryService(IEntryRepository repo, ILogger<EntryService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task HandleMessage(Message<Entry> message)
        {
            await _repo.Persist(message.Data);
        }
    }
}
