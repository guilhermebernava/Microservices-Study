namespace RestaurantService.Entities;

public class OrderItem
{
    public OrderItem(int orderId, int itemId)
    {
        OrderId = orderId;
        ItemId = itemId;
    }

    public int OrderId { get; set; }
    public int ItemId { get; set; }
}
