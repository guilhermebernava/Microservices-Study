using ItensService.Context;
using ItensService.Mappers;
using ItensService.Repositories;
using ItensService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddDbContext<ItemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ItemContext).Assembly.FullName));
}, ServiceLifetime.Singleton);
builder.Services.AddScoped<IItemRepository,ItemRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
