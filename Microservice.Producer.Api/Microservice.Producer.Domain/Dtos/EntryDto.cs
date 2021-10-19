using Microservice.Producer.Domain.Entities;
using System;

namespace Microservice.Producer.Domain.Dtos
{
    public class EntryDto
    {
        public string UserName { get; set; }
        public DateTime Moment { get; set; }
        public decimal Value { get; set; }
        public char Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
    }
}
