using RestaurantService.Entities;

namespace RestaurantService.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    public Task<Order> GetLastInsertAsync();
}
