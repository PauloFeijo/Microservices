using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.MessageBroker;
using Microservice.Consumer.Domain.Interfaces.Repositories;
using Microservice.Consumer.Domain.Interfaces.Services;
using Microservice.Consumer.Domain.Services;
using Microservice.Consumer.Infra.Data.Mongo.Context;
using Microservice.Consumer.Infra.Data.Mongo.Repositories;
using Microservice.Consumer.Infra.MessagingBroker.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Consumer.Infra.IoC
{
    public static class ModuleExtension
    {
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
                .AddSingleton(x => new MongoContext(configuration.GetConnectionString("MicroservicesDb"), "Microservices"));
    }
}
