using ItensService.Services.CreateItem;
using ItensService.Services.GeByIdItem;

namespace ItensService.Services;

public static class ServicesInector
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IItemCreateService, ItemCreateService>();
        services.AddScoped<IItemGetByIdService, ItemGetByIdService>();
    }
}