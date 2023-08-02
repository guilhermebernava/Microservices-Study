using Microsoft.AspNetCore.Mvc;
using RestaurantService.Models;
using RestaurantService.Repositories;
using RestaurantService.Services.CreateOrder;
using RestaurantService.Services.CreateOrderRabbitMq;

namespace RestaurantService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    [Route("HTTP")]
    public async Task<IActionResult> AddWithHttpAsync([FromServices] ICreateOrderService service, [FromBody] OrderModel viewModel)
    {
        var result = await service.Execute(viewModel);
        if (result)
        {
            return Ok();

        }
        return BadRequest();
    }

    [HttpPost]
    [Route("RabbitMq")]
    public async Task<IActionResult> AddWithRabbitMqAsync([FromServices] ICreateOrderRabbitMq service, [FromBody] OrderModel viewModel)
    {
        var result = await service.Publish(viewModel);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromServices] IOrderRepository orderRepository, [FromServices] IOrderItemRepository orderItemRepository)
    {
        var orders = await orderRepository.GetAll();
        var orderItens = await orderItemRepository.GetAll();
       
        return Ok(new { orders, orderItens });
    }

}
