using RestaurantService.Context;
using RestaurantService.Entities;

namespace RestaurantService.Repositories;

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(RestaurantContext context) : base(context)
    {
    }
}
