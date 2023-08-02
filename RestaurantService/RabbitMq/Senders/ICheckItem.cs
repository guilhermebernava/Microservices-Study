namespace RestaurantService.RabbitMq.Senders;

public interface ICheckItemRabbitMq
{
    public void Execute(int itemId,int orderId);
}
