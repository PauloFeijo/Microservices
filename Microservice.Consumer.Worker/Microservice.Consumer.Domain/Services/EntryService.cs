using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces;
using Microservice.Consumer.Domain.Messages;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repo;

        public EntryService(IEntryRepository repo)
        {
            _repo = repo;
        }

        public async Task HandleMessage(Message<Entry> message)
        {
            await _repo.Persist(message.Data);
        }
    }
}
