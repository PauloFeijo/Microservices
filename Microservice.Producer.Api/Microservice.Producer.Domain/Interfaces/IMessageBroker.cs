using Microservice.Producer.Domain.Messages;
using System.Threading.Tasks;

namespace Microservice.Producer.Domain.Interfaces
{
    public interface IMessageBroker
    {
        Task Publish<TData>(Message<TData> message);
    }
}
