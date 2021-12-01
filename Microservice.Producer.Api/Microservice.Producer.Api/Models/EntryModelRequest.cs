using Microservice.Producer.Domain.Entities;
using System;

namespace Microservice.Producer.Api.Models
{
    public class EntryModelRequest
    {
        public string UserName { get; set; }
        public DateTime Moment { get; set; }
        public decimal Value { get; set; }
        public char Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }

        public Entry ToEntry()
        {
            return new Entry()
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                Moment = Moment,
                Value = Value,
                Type = Type,
                AccountDescription = AccountDescription,
                Description = Description,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
