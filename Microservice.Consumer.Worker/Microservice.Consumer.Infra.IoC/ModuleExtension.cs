using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.MessageBroker;
using Microservice.Consumer.Domain.Interfaces.Repositories;
using Microservice.Consumer.Domain.Interfaces.Services;
using Microservice.Consumer.Domain.Services;
using Microservice.Consumer.Infra.Data.Context;
using Microservice.Consumer.Infra.Data.Repositories;
using Microservice.Consumer.Infra.MessagingBroker.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Consumer.Infra.IoC
{
    public static class ModuleExtension
    {
        private const string ConnectionStringName = "MicroservicesDb";
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton<IEntryService, EntryService>()
                .AddSingleton<IEntryRepository, EntryRepository>()
                .RegisterMessageBroker(configuration)
                .RegisterDataBaseConnection(configuration);

        private static IServiceCollection RegisterMessageBroker(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(new RabbitConfig(configuration))
                .AddSingleton<IMessageBrokerService<Entry>, RabbitMqService<Entry>>();

        private static IServiceCollection RegisterDataBaseConnection(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<IContext, Context>(op =>
                    op.UseSqlServer(configuration.GetConnectionString(ConnectionStringName)), ServiceLifetime.Singleton);
    }
}
