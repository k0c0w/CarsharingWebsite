using Carsharing.Forms;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Area("Api")]
public class CarModelController : Controller
{
    private readonly CarsharingContext _carsharingContext;

    public CarModelController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]CarModelDto dto)
    {
        var newCarModel = new CarModel
        {
            Brand = dto.Brand,
            Model = dto.Model,
            Description = dto.Description,
            TariffId = dto.TariffId
        };
        await _carsharingContext.AddAsync(newCarModel);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Existing()
    {
        var carModels = await _carsharingContext.CarModels.ToArrayAsync();
        return Json(carModels.Select(x => new { id = x.Id, brand = x.Brand, 
            model=x.Model, tariff_id = x.TariffId }));
    }
}