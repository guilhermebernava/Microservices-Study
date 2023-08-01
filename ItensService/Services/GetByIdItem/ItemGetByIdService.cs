using ItensService.Entities;
using ItensService.Repositories;
using ItensService.Services.CreateItem;

namespace ItensService.Services.GeByIdItem;

public class ItemGetByIdService : IItemGetByIdService
{
    public ItemGetByIdService(IItemRepository repository)
    {
        Repository = repository;
    }

    private IItemRepository Repository { get; set; }


    public async Task<Item> GetByIdAsync(int id)
    {
        return await Repository.GetByIdAsync(id);
    }
}
