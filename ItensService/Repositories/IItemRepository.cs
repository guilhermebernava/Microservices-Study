using ItensService.Entities;

namespace ItensService.Repositories;

public interface IItemRepository
{
    public Task<bool> AddAsync(Item item);
    public Task<bool> UpdateAsync(Item item,int id);
    public Task<bool> DeleteAsync(int id);
    public Task<List<Item>> GetAllAsync();
    public Task<Item?> GetByIdAsync(int id);
}
