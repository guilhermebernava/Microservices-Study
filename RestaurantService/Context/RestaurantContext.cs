using Microsoft.EntityFrameworkCore;
using RestaurantService.Context.Configurations;
using RestaurantService.Entities;

namespace RestaurantService.Context;

public class RestaurantContext : DbContext
{
    public DbSet<Order> Orders;
    public DbSet<OrderItem> OrdersItem;
    public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
