﻿using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Producer.Infra.MessagingBroker.RabbitMq
{
    [ExcludeFromCodeCoverage]
    public class RabbitConfig
    {
        public RabbitConfig(IConfiguration configuration)
        {
            HostName = configuration["RabbitConfig:HostName"];
            Port = int.Parse(configuration["RabbitConfig:Port"]);
            UserName = configuration["RabbitConfig:UserName"];
            Password = configuration["RabbitConfig:Password"];
            Queue = configuration["RabbitConfig:Queue"];
            Exchange = configuration["RabbitConfig:Exchange"];
        }

        public string HostName { get; }
        public int Port { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Queue { get; }
        public string Exchange { get; }
    }
}
