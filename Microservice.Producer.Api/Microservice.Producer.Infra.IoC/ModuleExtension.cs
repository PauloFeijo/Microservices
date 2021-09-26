using Microservice.Producer.Domain.Interfaces;
using Microservice.Producer.Domain.Services;
using Microservice.Producer.Infra.MessagingBroker.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Producer.Infra.IoC
{
    public static class ModuleExtension
    { 
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IEntryService, EntryService>()
                .RegisterMessageBroker(configuration);

        private static IServiceCollection RegisterMessageBroker(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(new RabbitConfig(configuration))
                .AddScoped<IMessageBroker, RabbitMq>();
    }
}
