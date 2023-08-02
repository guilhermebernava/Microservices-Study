using ItensService.Dtos;
using ItensService.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using IModel = RabbitMQ.Client.IModel;

namespace ItensService.RabbitMq;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMqSubscriber(IConfiguration configuration,IServiceScopeFactory scopeFactory)
    {
        _configuration = configuration;
        _connection = new ConnectionFactory() { HostName = _configuration["RabbitMqHost"], Port = int.Parse(_configuration["RabbitMqPort"]), UserName = "admin", Password = "1234" }.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        EventingBasicConsumer? consumer = new(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            ReadOnlyMemory<byte> body = ea.Body;
            string? mensagem = Encoding.UTF8.GetString(body.ToArray());
            var dto = JsonSerializer.Deserialize<SubscriberDto>(mensagem) ?? throw new Exception("Error in deserialize DTO");

            //check se item existe
            using var scope = _scopeFactory.CreateScope();
            var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

            var existItem = itemRepository.GetByIdAsync(dto.ItemId);

            if(existItem != null)
            {
                _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            }
        };

        _channel.BasicConsume(_queueName, autoAck: true, consumer);

        return Task.CompletedTask;
    }
}
