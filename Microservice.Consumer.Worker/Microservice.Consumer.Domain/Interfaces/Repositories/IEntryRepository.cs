using Microservice.Consumer.Domain.Entities;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Interfaces.Repositories
{
    public interface IEntryRepository
    {
        Task Persist(Entry entry);
    }
}
