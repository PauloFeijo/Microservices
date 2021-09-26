using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Messages;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Interfaces
{
    public interface IEntryService
    {
        Task HandleMessage(Message<Entry> message);
    }
}
