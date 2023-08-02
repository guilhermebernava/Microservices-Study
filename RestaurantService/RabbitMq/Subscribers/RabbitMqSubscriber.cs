using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestaurantService.Dtos;
using RestaurantService.Repositories;
using System.Text;
using System.Text.Json;

namespace RestaurantService.RabbitMq.Subscribers;

//Diz que ele vai ser um servico de segundo plano
public class RabbitMqSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMqSubscriber(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    {
        _configuration = configuration;
        //abrindo uma conexao com rabbitMq
        _connection = new ConnectionFactory() { HostName = _configuration["RabbitMqHost"], Port = int.Parse(_configuration["RabbitMqPort"]), UserName = "admin", Password = "1234" }.CreateConnection();
        //criando o canal
        _channel = _connection.CreateModel();
        //criando a fila e pegando seu nome
        _queueName = _channel.QueueDeclare(queue: "order_queue").QueueName;
        //conectando a fila com o canal, via trigger
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        EventingBasicConsumer? consumer = new(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            ReadOnlyMemory<byte> body = ea.Body;
            string? mensagem = Encoding.UTF8.GetString(body.ToArray());
            SubscriberDto dto = JsonSerializer.Deserialize<SubscriberDto>(mensagem) ?? throw new Exception("Error in deserialize DTO");

            using var scope = _scopeFactory.CreateScope();
            var orderItemRepository = scope.ServiceProvider.GetRequiredService<IOrderItemRepository>();

            var existItem = orderItemRepository.AddAsync(new Entities.OrderItem(dto.OrderId,dto.ItemId));
        };

        _channel.BasicConsume(_queueName, autoAck: true, consumer);
        return Task.CompletedTask;
    }
}
