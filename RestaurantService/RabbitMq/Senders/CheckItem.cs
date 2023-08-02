using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RestaurantService.Dtos;

namespace RestaurantService.RabbitMq.Senders;

public class CheckItemRabbitMq : ICheckItemRabbitMq
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public CheckItemRabbitMq(IConfiguration configuration)
    {
        _configuration = configuration;
        //vai criar uma conexao com o rabbitMq
        _connection = new ConnectionFactory() { HostName = _configuration["RabbitMqHost"], Port = int.Parse(_configuration["RabbitMqPort"]), UserName = "admin", Password = "1234" }.CreateConnection();
        _channel = _connection.CreateModel();

        //vai especificar a forma de troca de dados
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
    }

    public void Execute(int itemId, int orderId)
    {
        string mensagem = JsonSerializer.Serialize(new SubscriberDto(itemId,orderId));
        var body = Encoding.UTF8.GetBytes(mensagem);
        //vai enviar a mensagem pelo rabbitMq
        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
    }
}
