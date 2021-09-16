using Microservice.Query.Domain.Dtos;
using Microservice.Query.Domain.Interfaces;
using Microservice.Query.Infra.Data.Context;
using Microservice.Query.Infra.Data.SqlCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Query.Infra.Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private IContext _ctx;

        public EntryRepository(IContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<EntryDto> GetEntry(Guid id)
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
