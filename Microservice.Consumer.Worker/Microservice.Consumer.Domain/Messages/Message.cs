using System;

namespace Microservice.Consumer.Domain.Messages
{
    public class Message<TData>
    {
        public Guid Id { get; set; }
        public TData Data { get; set; }
    }
}
