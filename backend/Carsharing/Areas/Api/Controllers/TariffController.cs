using Carsharing.Forms;
using Entities.Model;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Area("Api")]
public class TariffController : Controller
{
    private readonly CarsharingContext _carsharingContext;

    public TariffController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }

    [HttpPost]
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

    [HttpGet]
    public async Task<IActionResult> Tariffs()
    {
        var tariffs = await _carsharingContext.Tariffs.Where(x => x.IsActive).ToArrayAsync();
            
        return Json(tariffs.Select(x => new
        {
            id = x.Id, name = x.Name, price = x.Price, description = x.Description
            //todo: добавить полную ссылку до изображения image_url
        }));
    }
}