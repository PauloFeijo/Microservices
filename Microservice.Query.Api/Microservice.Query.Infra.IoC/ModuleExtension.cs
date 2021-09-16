using Microservice.Query.Domain.Interfaces;
using Microservice.Query.Infra.Data.Context;
using Microservice.Query.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Query.Infra.IoC
{
    public static class ModuleExtension
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration) =>
            services
                .RegisterConnection(configuration)
                .AddScoped<IEntryRepository, EntryRepository>();

        private static IServiceCollection RegisterConnection(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IContext>(_ => new Context(configuration.GetConnectionString("ConnectionString")));

    }
}
