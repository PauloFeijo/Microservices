﻿using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.Repositories;
using Microservice.Consumer.Domain.Interfaces.Services;
using Microservice.Consumer.Domain.Messages;
using System;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repo;

        public EntryService(IEntryRepository repo)
        {
            _repo = repo;
        }

        public async Task HandleMessage(Message<Entry> message)
        {
            var entry = message.Data;
            entry.CreatedAt = DateTime.Now;
            await _repo.Persist(entry);
        }
    }
}
