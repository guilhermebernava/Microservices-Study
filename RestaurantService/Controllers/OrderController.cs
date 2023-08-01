using Microsoft.AspNetCore.Mvc;
using RestaurantService.Models;
using RestaurantService.Services.CreateOrder;

namespace RestaurantService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromServices] ICreateOrderService service, [FromBody] OrderModel viewModel)
    {
        var result = await service.CreateAsync(viewModel);
        if (result)
        {
            return Ok();

        }
        return BadRequest();
    }

}
