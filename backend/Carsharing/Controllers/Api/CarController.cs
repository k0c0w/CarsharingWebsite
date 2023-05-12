using Carsharing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Exceptions;

namespace Carsharing.Controllers;


[Route("api/cars")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService service)
    {
        _carService = service;
    }
    
    [HttpGet("models/{tariff:int}")]
    public async Task<IActionResult> GetCarModelsByTariff([FromRoute] int tariff)
    {
        var models = await _carService.GetModelsByTariffIdAsync(tariff);

        if (!models.Any()) return NotFound();
        return new JsonResult(models.Select(x => new CarModelVM
        {
            Id = x.Id,
            Brand = x.Brand,
            Description = x.Description,
            Model = x.Model,
            Url = x.ImageUrl,
            TariffId = tariff
        }));
    }
    
    [HttpGet("model/{tariff:int}")]
    public async Task<IActionResult> GetCarModelByTariff([FromRoute] int tariff)
    {
        try
        {
            var model = await _carService.GetModelByTariffIdAsync(tariff);
            return new JsonResult(new CarModelVM
            {
                Brand = model.Brand,
                Description = model.Description,
                Model = model.Model,
                Url = model.ImageUrl,
                TariffId = tariff,
                Id = model.Id
            });
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpGet("{modelId:int}")]
    public async Task<IActionResult> GetAvailableCarsByModelId([FromRoute] int modelId)
    {
       var cars = await _carService.GetAvailableCarsByModelAsync(modelId);
       return new JsonResult(cars.Select(x => new CarVM
       {
           Id = x.Id,
           LicensePlate = x.LicensePlate,
           ParkingLatitude = x.ParkingLatitude,
           ParkingLongitude = x.ParkingLongitude
       }));
    }

    [HttpGet("rent")]
    [Authorize(Policy = "CanBuy")]
    public async Task<IActionResult> RentCar()
    {
        return Ok("You brought it!");
    }
}