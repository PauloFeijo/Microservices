using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Query.Infra.Data.Mongo.Context
{
    [ExcludeFromCodeCoverage]
    public class MongoContext
    {
        public MongoContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            MongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoDatabase MongoDatabase { get; private set; }
    }
}
