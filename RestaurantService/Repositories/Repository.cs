using Microsoft.EntityFrameworkCore;
using RestaurantService.Context;

namespace RestaurantService.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    public Repository(RestaurantContext context)
    {
        this.Context = context;
        DbSet = Context.Set<T>();
    }

    private RestaurantContext Context { get; set; }
    public DbSet<T> DbSet { get; set; }

    public async Task<bool> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return await SaveAsync();
    }

    private async Task<bool> SaveAsync() => await Context.SaveChangesAsync() == 1;

    public async Task<List<T>> GetAll()
    {
        return await DbSet.ToListAsync();
    }
}
