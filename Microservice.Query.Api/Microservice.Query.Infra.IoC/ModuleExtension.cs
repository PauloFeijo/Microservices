using Microservice.Query.Domain.Interfaces.Repositories;
using Microservice.Query.Infra.Data.Mongo.Context;
using Microservice.Query.Infra.Data.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Query.Infra.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ModuleExtension
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration) =>
            services
                .RegisterConnection(configuration)
                .AddScoped<IEntryRepository, EntryRepository>();

        private static IServiceCollection RegisterConnection(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped(x => new MongoContext(configuration.GetConnectionString("MicroservicesDb"), "Microservices"));

    }
}
