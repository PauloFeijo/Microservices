using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Interfaces
{
    public interface IMessageBroker<TData> where TData : Entity
    {
        IMessageBroker<TData> ConfigureConsumer(Func<Message<TData>, Task> consumerDelegate);
        Task StartConsume(CancellationToken cancellationToken);
    }
}
