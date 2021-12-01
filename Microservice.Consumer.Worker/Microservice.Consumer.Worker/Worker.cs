using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.MessageBroker;
using Microservice.Consumer.Domain.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Consumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IMessageBrokerService<Entry> _messageBroker;
        private readonly IEntryService _entryService;
        private readonly ILogger<Worker> _logger;

        public Worker(IMessageBrokerService<Entry> messageBroker, IEntryService entryService, ILogger<Worker> logger)
        {
            _messageBroker = messageBroker;
            _entryService = entryService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Work started");

                await _messageBroker
                    .ConfigureConsumer(message => _entryService.HandleMessage(message))
                    .StartConsume(stoppingToken);

                _logger.LogInformation("Work finished");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Worker finished with errors");
            }
        }
    }
}
