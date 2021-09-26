using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces;
using Microservice.Consumer.Domain.Services;
using Microservice.Consumer.Infra.MessagingBroker.RabbitMq;
using Microservice.Consumer.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microservice.Consumer.Infra.Data.Context;

namespace Microservice.Consumer.Infra.IoC
{
    public static class ModuleExtension
    {
        private const string ConnectionStringName = "ConnectionString";
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton<IEntryService, EntryService>()
                .AddSingleton<IEntryRepository, EntryRepository>()
                .RegisterMessageBroker(configuration)
                .RegisterDataBaseConnection(configuration);

        private static IServiceCollection RegisterMessageBroker(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(new RabbitConfig(configuration))
                .AddSingleton<IMessageBroker<Entry>, RabbitMq<Entry>>();

        private static IServiceCollection RegisterDataBaseConnection(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<IContext, Context>(op => 
                    op.UseSqlServer(configuration.GetConnectionString(ConnectionStringName)), ServiceLifetime.Singleton);
    }
}
