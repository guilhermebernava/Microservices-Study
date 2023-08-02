using ItensService.Context;
using ItensService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItensService.Repositories;

public class ItemRepository : IItemRepository
{
    public ItemRepository(ItemContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<Item>();
    }

    private ItemContext DbContext { get; set; }
    private DbSet<Item> DbSet { get; set; }


    public async Task<bool> AddAsync(Item item)
    {
        try
        {
            DbSet.Add(item);
            return await SaveAsync();
        }
        catch(Exception ex) 
        {
            var teste = ex.Message;
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);

            if(entity == null)
            {
                return false;
            }

            DbSet.Remove(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public Task<List<Item>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(_ => _.Id == id);
        return entity;
    }

    public async Task<bool> UpdateAsync(Item item, int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            entity.Name = item.Name;
            entity.Value = item.Value;

            DbSet.Update(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> SaveAsync()
    {
        return await DbContext.SaveChangesAsync() == 1;
    }
}
