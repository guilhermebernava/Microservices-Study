using Microsoft.EntityFrameworkCore;
using RestaurantService.Context;
using RestaurantService.Http;
using RestaurantService.RabbitMq.Senders;
using RestaurantService.RabbitMq.Subscribers;
using RestaurantService.Repositories;
using RestaurantService.Services.CreateOrder;
using RestaurantService.Services.CreateOrderRabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddDbContext<RestaurantContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(RestaurantContext).Assembly.FullName));
}, ServiceLifetime.Singleton);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddScoped<ICreateOrderRabbitMq, CreateOrderRabbitMq>();
builder.Services.AddHttpClient<ICheckItemHttp, CheckItemHttp>();
builder.Services.AddSingleton<ICheckItemRabbitMq, CheckItemRabbitMq>();

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
