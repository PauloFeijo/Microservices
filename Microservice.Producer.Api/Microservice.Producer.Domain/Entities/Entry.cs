﻿using System;

namespace Microservice.Producer.Domain.Entities
{
    public class Entry
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Moment { get; set; }
        public decimal Value { get; set; }
        public char Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
