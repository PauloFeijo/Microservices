using Microservice.Producer.Domain.Interfaces.MessageBroker;
using Microservice.Producer.Domain.Interfaces.Services;
using Microservice.Producer.Domain.Services;
using Microservice.Producer.Infra.MessagingBroker.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Producer.Infra.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ModuleExtension
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IEntryService, EntryService>()
                .RegisterMessageBroker(configuration);

        private static IServiceCollection RegisterMessageBroker(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(new RabbitConfig(configuration))
                .AddScoped<IMessageBrokerService, RabbitMqService>();
    }
}
