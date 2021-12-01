using Microservice.Consumer.Domain.Entities;
using Microservice.Consumer.Domain.Interfaces.MessageBroker;
using Microservice.Consumer.Domain.Messages;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Consumer.Infra.MessagingBroker.RabbitMq
{
    public class RabbitMqService<TData> : IMessageBrokerService<TData> where TData : Entity
    {
        private readonly RabbitConfig _config;
        private readonly ILogger<RabbitMqService<TData>> _logger;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private Func<Message<TData>, Task> _consumerDelegate;

        public RabbitMqService(RabbitConfig rabbitConfig, ILogger<RabbitMqService<TData>> logger)
        {
            _config = rabbitConfig;
            _logger = logger;
        }

        public IMessageBrokerService<TData> ConfigureConsumer(Func<Message<TData>, Task> consumerDelegate)
        {
            _consumerDelegate = consumerDelegate;
            return this;
        }

        public async Task StartConsume(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(GetChannel());
            await RegisterConsumer(consumer);

            while (!cancellationToken.IsCancellationRequested)
            {
                GetChannel().BasicConsume
                (
                    queue: _config.Queue,
                    autoAck: false,
                    consumer: consumer
                );
            }
        }

        private async Task RegisterConsumer(EventingBasicConsumer consumer) =>
            await Task.Run(() => consumer.Received += async (model, ea) =>
            {
                var deliveryTag = ea.DeliveryTag;
                try
                {
                    var message = DeserializeAndCreateMessage(ea.Body.ToArray());
                    await Consume(message, deliveryTag);
                }
                catch (Exception)
                {
                    Reject(deliveryTag);
                }
            });

        private async Task Consume(Message<TData> message, ulong deliveryTag)
        {
            try
            {
                _logger.LogInformation($"[RabbitMq] Message tag {deliveryTag} received");
                await _consumerDelegate.Invoke(message);
                Ack(deliveryTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[RabbitMq] Error on consume message tag {deliveryTag}");
                throw;
            }
        }

        private void Ack(ulong deliveryTag)
        {
            try
            {
                GetChannel().BasicAck(deliveryTag, false);
                _logger.LogInformation($"[RabbitMq] Acknowledged message tag {deliveryTag}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[RabbitMq] Error on ack message tag {deliveryTag}");
                throw;
            }
        }

        private void Reject(ulong deliveryTag)
        {
            try
            {
                GetChannel().BasicReject(deliveryTag, false);
                _logger.LogInformation($"[RabbitMq] Rejected message tag {deliveryTag}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[RabbitMq] Error on reject message tag {deliveryTag}");
                throw;
            }
        }

        private Message<TData> DeserializeAndCreateMessage(byte[] body)
        {
            try
            {
                var data = JsonSerializer.Deserialize<TData>(body);
                return new Message<TData>()
                {
                    Id = data.Id,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[RabbitMq] Error on deserialize message received.");
                throw;
            }
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
