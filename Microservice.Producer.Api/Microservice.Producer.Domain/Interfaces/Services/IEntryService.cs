using Microservice.Producer.Domain.Entities;

namespace Microservice.Producer.Domain.Interfaces.Services
{
    public interface IEntryService
    {
        void PublishEntry(Entry entry);
    }
}
