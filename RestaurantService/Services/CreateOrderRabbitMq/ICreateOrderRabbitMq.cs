using RestaurantService.Models;

namespace RestaurantService.Services.CreateOrderRabbitMq;

public interface ICreateOrderRabbitMq
{
    public Task<bool> Publish(OrderModel model);
}
