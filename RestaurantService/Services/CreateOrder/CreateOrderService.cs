using RestaurantService.Entities;
using RestaurantService.Http;
using RestaurantService.Models;
using RestaurantService.Repositories;

namespace RestaurantService.Services.CreateOrder;

public class CreateOrderService : ICreateOrderService
{
    public CreateOrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ICheckItemHttp check)
    {
        OrderRepository = orderRepository;
        OrderItemRepository = orderItemRepository;
        CheckItemHttp = check;
    }

    private IOrderRepository OrderRepository { get; set; }
    private IOrderItemRepository OrderItemRepository { get; set; }
    private ICheckItemHttp CheckItemHttp { get; set; }


    public async Task<bool> Execute(OrderModel model)
    {
        foreach (var item in model.ItensId)
        {
            var result = await CheckItemHttp.Send(item);

            if (!result)
            {
                return false;
            }
        }

        await OrderRepository.AddAsync(new Order());
        var orderEntity = await OrderRepository.GetLastInsertAsync();

        if (orderEntity == null)
        {
            return false;
        }

        foreach (var item in model.ItensId)
        {
            if (!await OrderItemRepository.AddAsync(new OrderItem(orderEntity.Id, item)))
            {
                return false;
            };
        }

        return true;
    }

}
