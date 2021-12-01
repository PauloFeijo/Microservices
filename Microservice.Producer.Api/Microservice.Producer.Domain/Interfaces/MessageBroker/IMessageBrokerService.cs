using Microservice.Producer.Domain.Messages;

namespace Microservice.Producer.Domain.Interfaces.MessageBroker
{
    public interface IMessageBrokerService
    {
        void Publish<TData>(Message<TData> message);
    }
}
