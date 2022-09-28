using System;

namespace Microservice.Consumer.Domain.Messages
{
    public class Message<TData>
    {
        public TData Data { get; set; }
    }
}
