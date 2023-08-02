namespace RestaurantService.Dtos;

public class SubscriberDto
{
    public SubscriberDto(int itemId, int orderId)
    {
        ItemId = itemId;
        OrderId = orderId;
    }

    public int ItemId { get; private set; }
    public int OrderId { get; private set; }
}
