using Microservice.Producer.Domain.Dtos;
using System.Threading.Tasks;

namespace Microservice.Producer.Domain.Interfaces
{
    public interface IEntryService
    {
        Task PublishEntry(EntryDto entryDto);
    }
}
