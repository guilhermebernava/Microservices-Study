using ItensService.Models;
using ItensService.Services.CreateItem;
using Microsoft.AspNetCore.Mvc;

namespace ItensService.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromServices] IItemCreateService service, [FromBody] ItemModel viewModel)
    {
        var result = await service.CreateAsync(viewModel);
        if (result)
        {
            return Created(nameof(GetByIdAsync), 1);

        }
        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync([FromServices] IItemGetByIdService service, [FromQuery] int id)
    {
        var result = await service.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();

        }
        return Ok(result);
    }
}
