using AutoMapper;
using ItensService.Entities;
using ItensService.Models;

namespace ItensService.Mappers;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<ItemModel, Item>();
        CreateMap<Item, ItemModel>();
    }
}