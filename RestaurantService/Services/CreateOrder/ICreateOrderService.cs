using RestaurantService.Models;

namespace RestaurantService.Services.CreateOrder;

public interface ICreateOrderService
{
    public Task<bool> Execute(OrderModel model);
}
