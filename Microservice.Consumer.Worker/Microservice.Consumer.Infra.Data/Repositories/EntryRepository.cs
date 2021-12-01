using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.Repositories;
using Microservice.Consumer.Infra.Data.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microservice.Consumer.Infra.Data.Repositories
{
    public class EntryRepository : BaseRepository<Entry>, IEntryRepository
    {
        private readonly ILogger<EntryRepository> _logger;

        public EntryRepository(IContext context, ILogger<EntryRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task Persist(Entry entry)
        {
            try
            {
                await ExecuteAsync(dbSet => dbSet.AddAsync(entry));
                _logger.LogInformation($"Entry id {entry.Id} persisted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erros on persist entry id {entry.Id}");
                throw;
            }
        }
    }
}
