namespace RestaurantService.Entities;

public class Order
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
