using Microservice.Producer.Domain.Entities;
using System.Threading.Tasks;

namespace Microservice.Producer.Domain.Interfaces
{
    public interface IEntryService
    {
        Task PublishEntry(Entry entry);
    }
}
