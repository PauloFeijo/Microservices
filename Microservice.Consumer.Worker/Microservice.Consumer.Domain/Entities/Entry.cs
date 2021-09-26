using System;

namespace Microservice.Consumer.Domain.Entities
{
    public class Entry : Entity
    {
        public string UserName { get; set; }
        public DateTime Moment { get; set; }
        public decimal Value { get; set; }
        public char Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }

        public Entry()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
