using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice.Query.Infra.Data.Context
{
    public interface IContext
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null);
    }
}
