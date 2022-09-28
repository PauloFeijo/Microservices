using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.Repositories;
using Microservice.Consumer.Infra.Data.Mongo.Context;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Consumer.Infra.Data.Mongo.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EntryRepository : IEntryRepository
    {
        private readonly IMongoCollection<Entry> _entryCollection;

        public EntryRepository(MongoContext ctx)
        {
            _entryCollection = ctx.MongoDatabase.GetCollection<Entry>("Entry");
        }

        public async Task Persist(Entry entry)
        {
            await _entryCollection.InsertOneAsync(entry);
        }
    }
}
