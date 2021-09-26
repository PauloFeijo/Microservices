using Microservice.Producer.Domain.Interfaces;
using Microservice.Producer.Domain.Messages;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservice.Producer.Infra.MessagingBroker.RabbitMq
{
    public class RabbitMq : IMessageBroker
    {
        private readonly RabbitConfig _config;
        private readonly ILogger<RabbitMq> _logger;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMq(RabbitConfig rabbitConfig, ILogger<RabbitMq> logger)
        {
            _config = rabbitConfig;
            _logger = logger;
        }

        public async Task Publish<TData>(Message<TData> message)
        {
            _logger.LogInformation($"publish in queue {_config.Queue} RabbitMq. Id: {message.Id}");

            var messageSerialized = JsonSerializer.Serialize(message.Data);
            var body = Encoding.UTF8.GetBytes(messageSerialized);

            GetChannel().BasicPublish(
                exchange: _config.Exchange,
                routingKey: string.Empty,
                basicProperties: null,
                body: body
            );
        }

        private IModel GetChannel()
        {
            if (_channel == null)
            {
                _connectionFactory = new ConnectionFactory() 
                { 
                    HostName = _config.HostName,
                    Port = _config.Port,
                    UserName = _config.UserName,
                    Password = _config.Password
                };
                _connection = _connectionFactory.CreateConnection();
                _channel = _connection.CreateModel();
                DeclareQueue();
            }

            return _channel;
        }

        private void DeclareQueue()
        {
            _channel.QueueDeclare
            (
                queue: _config.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            _channel.ExchangeDeclare
            (
                exchange: _config.Exchange,
                type: "direct",
                durable: true,
                autoDelete: false,
                arguments: null
            );

            _channel.QueueBind
            (
                queue: _config.Queue,
                exchange: _config.Exchange,
                routingKey: "",
                arguments: null
            );
        }
    }
}
