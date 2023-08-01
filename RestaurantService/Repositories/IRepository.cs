namespace RestaurantService.Repositories;

public interface IRepository<T> where T : class
{
    public Task<bool> AddAsync(T entity);
}
