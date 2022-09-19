using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microservice.Query.Infra.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class Context : IContext
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public Context(string connectionSttring)
        {
            _connectionString = connectionSttring;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return await GetConnection().QueryAsync<T>(sql, param);
        }

        private IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
    }
}
