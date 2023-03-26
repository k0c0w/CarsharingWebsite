using Carsharing.Forms;
using Carsharing.Model;
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
        var id = await _carsharingContext.Tarrifs.CountAsync() + 1;
        var newTariff = new Tarrif
        {
            Id = id,
            Price = dto.Price,
            Name = dto.Name,
            Period = dto.Period,
            Description = dto.Description
        };
        await _carsharingContext.AddAsync(newTariff);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }
}