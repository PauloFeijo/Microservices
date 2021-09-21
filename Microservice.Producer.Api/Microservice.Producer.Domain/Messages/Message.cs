using System;

namespace Microservice.Producer.Domain.Messages
{
    public class Message<TData>
    {
        public Guid Id { get; set; }
        public TData Data { get; set; }

        public Message(TData data)
        {
            Id = Guid.NewGuid();
            Data = data;
        }
    }
}
