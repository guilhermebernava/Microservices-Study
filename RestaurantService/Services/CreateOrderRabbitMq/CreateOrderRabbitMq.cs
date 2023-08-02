using RestaurantService.Entities;
using RestaurantService.Models;
using RestaurantService.RabbitMq.Senders;
using RestaurantService.Repositories;

namespace RestaurantService.Services.CreateOrderRabbitMq;

public class CreateOrderRabbitMq : ICreateOrderRabbitMq
{
    public CreateOrderRabbitMq(IOrderRepository orderRepository ,ICheckItemRabbitMq checkItemRabbitMq)
    {
        OrderRepository = orderRepository;
        CheckItemRabbitMq = checkItemRabbitMq;
    }

    private IOrderRepository OrderRepository { get; set; }
    private ICheckItemRabbitMq CheckItemRabbitMq { get; set; }

    public async Task<bool> Publish(OrderModel model)
    {
        await OrderRepository.AddAsync(new Order());
        var orderEntity = await OrderRepository.GetLastInsertAsync();

        foreach (var itemId in model.ItensId)
        {
            CheckItemRabbitMq.Execute(itemId, orderEntity.Id);
        }

        return true;
    }
}
