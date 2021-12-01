using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Interfaces.MessageBroker
{
    public interface IMessageBrokerService<TData> where TData : Entity
    {
        
        IMessageBrokerService<TData> ConfigureConsumer(Func<Message<TData>, Task> consumerDelegate);
        Task StartConsume(CancellationToken cancellationToken);
    }
}
