using Microsoft.EntityFrameworkCore;
using RestaurantService.Context;
using RestaurantService.Entities;

namespace RestaurantService.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(RestaurantContext context) : base(context)
    {
    }

    public async Task<Order> GetLastInsertAsync()
    {
        var entity = await DbSet.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new Exception("Not found any Order");
        }

        return entity;
    }
}
