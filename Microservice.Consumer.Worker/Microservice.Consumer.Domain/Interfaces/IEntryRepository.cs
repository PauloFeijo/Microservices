using Microservice.Consumer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Consumer.Domain.Interfaces
{
    public interface IEntryRepository
    {
        Task Persist(Entry entry);
    }
}
