using Microservice.Query.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice.Query.Domain.Interfaces.Repositories
{
    public interface IEntryRepository
    {
        Task<IEnumerable<EntryDto>> GetEntries(EntryParamsDto param);
        Task<EntryDto> GetEntry(Guid id);
    }
}
