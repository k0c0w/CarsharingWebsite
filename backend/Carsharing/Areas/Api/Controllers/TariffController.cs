using Carsharing.Forms;
using Domain.Entities;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TariffController : ControllerBase
{
    private readonly CarsharingContext _carsharingContext;

    public TariffController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody]TariffDto dto)
    {
        var id = await _carsharingContext.Tariffs.CountAsync() + 1;
        var newTariff = new Tariff
        {
            Price = dto.Price,
            Name = dto?.Name,
            Description = dto?.Description
        };
        await _carsharingContext.AddAsync(newTariff);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Tariffs()
    {
        var tariffs = await _carsharingContext.Tariffs.Where(x => x.IsActive).ToArrayAsync();
            
        return new JsonResult(tariffs.Select(x => new { id = x.TariffId, name = x.Name, price = x.Price, description = x.Description }));
    }
}