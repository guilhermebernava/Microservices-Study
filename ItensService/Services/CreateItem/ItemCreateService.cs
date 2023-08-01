using AutoMapper;
using ItensService.Entities;
using ItensService.Models;
using ItensService.Repositories;

namespace ItensService.Services.CreateItem;

public class ItemCreateService : IItemCreateService
{
    public ItemCreateService(IMapper mapper, IItemRepository repository)
    {
        Mapper = mapper;
        Repository = repository;
    }

    private IMapper Mapper { get; set; }
    private IItemRepository Repository { get; set; }


    public async Task<bool> CreateAsync(ItemModel model)
    {
        var item = Mapper.Map<Item>(model);
        return await Repository.AddAsync(item);
    }
}
