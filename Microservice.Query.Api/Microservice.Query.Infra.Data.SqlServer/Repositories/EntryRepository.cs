using Microservice.Query.Domain.Dtos;
using Microservice.Query.Domain.Interfaces.Repositories;
using Microservice.Query.Infra.Data.SqlServer.Context;
using Microservice.Query.Infra.Data.SqlServer.SqlCommand;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Query.Infra.Data.SqlServer.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EntryRepository : IEntryRepository
    {
        private readonly IContext _ctx;

        public EntryRepository(IContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<EntryDto> GetEntry(string id)
        {
            var sql = SqlCommands.GetSqlEntryById();
            var param = new { Id = id };
            var result = await _ctx.QueryAsync<EntryDto>(sql, param);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<EntryDto>> GetEntries(EntryParamsDto param)
        {
            var sql = SqlCommands.GetSqlEntries(param);
            return await _ctx.QueryAsync<EntryDto>(sql, param);
        }
    }
}
