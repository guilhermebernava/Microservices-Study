using ItensService.Models;

namespace ItensService.Services.CreateItem;

public interface IItemCreateService
{
    public Task<bool> CreateAsync(ItemModel model);
}
