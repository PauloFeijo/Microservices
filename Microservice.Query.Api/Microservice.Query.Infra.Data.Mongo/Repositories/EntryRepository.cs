using AutoFixture;
using Microservice.Query.Domain.Dtos;
using Microservice.Query.Domain.Interfaces.Repositories;
using Microservice.Query.Infra.Data.Mongo.Context;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Query.Infra.Data.Mongo.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EntryRepository : IEntryRepository
    {
        private readonly IMongoCollection<EntryDto> _entryCollection;

        public EntryRepository(MongoContext ctx)
        {
            _entryCollection = ctx.MongoDatabase.GetCollection<EntryDto>("Entry");
        }

        public async Task<EntryDto> GetEntry(string id) => 
            await _entryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<EntryDto>> GetEntries(EntryParamsDto param)
        {
            var builder = Builders<EntryDto>.Filter;
            var filter = builder.Empty;
            
            if (param.UserName is not null) filter &= builder.Eq(x => x.UserName, param.UserName);
            if (param.InitialDate is not null) filter &= builder.Gte(x => x.CreatedAt, param.InitialDate);
            if (param.EndDate is not null) filter &= builder.Lte(x => x.CreatedAt, param.EndDate);
            if (param.Type is not null) filter &= builder.Eq(x => x.Type, param.Type);
            if (param.AccountDescription is not null) filter &= builder.Eq(x => x.AccountDescription, param.AccountDescription);
            if (param.Description is not null) filter &= builder.Eq(x => x.Description, param.Description);

            return await _entryCollection
                .Find(filter)
                .Skip((param.Page - 1) * param.PageSize)
                .Limit(param.PageSize)
                .ToListAsync();
        }
    }
}
