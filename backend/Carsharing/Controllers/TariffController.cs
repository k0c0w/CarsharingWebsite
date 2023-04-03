using Carsharing.Forms;
using Entities.Model;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TariffController : Controller
{
    private readonly CarsharingContext _carsharingContext;

    public TariffController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]TariffDto dto)
    {
        var id = await _carsharingContext.Tariffs.CountAsync() + 1;
        var newTariff = new Tariff
        {
            Price = dto.Price,
            Name = dto.Name,
            Description = dto.Description
        };
        await _carsharingContext.AddAsync(newTariff);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("tariffs")]
    public IActionResult GetExistingTariffs() => Json(_carsharingContext.Tariffs.Select(x => new
    {
        id = x.Id, name = x.Name, price = x.Price, description = x.Description
    }).ToList());
}