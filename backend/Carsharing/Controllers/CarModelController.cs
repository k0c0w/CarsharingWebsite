using Carsharing.Forms;
using Entities;
using Entities.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
//[Authorize(Policy = "HasEmail")]
[ApiController]
public class CarModelController : ControllerBase
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

    [HttpGet("getcar")]
    public async Task<IActionResult> GetCarModels()
    {
        var l = HttpContext.User.Claims;
        var carModels = await _carsharingContext.CarModels.ToArrayAsync();
        return new JsonResult(carModels.Select(x => new { id = x.Id, brand = x.Brand, model = x.Model, tariff_id = x.TariffId }));
    }

    [HttpGet("rent")]
    [Authorize(Policy = "CanBuy")]
    //[Authorize] , AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme
    public async Task<IActionResult> RentCar()
    {
        return Ok("You brought it!");
    }
}