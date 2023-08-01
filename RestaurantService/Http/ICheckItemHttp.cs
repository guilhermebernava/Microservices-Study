namespace RestaurantService.Http;

public interface ICheckItemHttp
{
    public Task<bool> Send(int itemId);
}
