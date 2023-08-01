using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RestaurantService.Entities;

namespace RestaurantService.Context.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.ItemId).HasColumnName("ItemId").IsRequired();
        builder.Property(x => x.OrderId).HasColumnName("OrderId").IsRequired();
        builder.HasKey("OrderId", "ItemId");
    }
}
