using ItensService.Entities;

namespace ItensService.Services.CreateItem;

public interface IItemGetByIdService
{
    public Task<Item> GetByIdAsync(int id);
}
