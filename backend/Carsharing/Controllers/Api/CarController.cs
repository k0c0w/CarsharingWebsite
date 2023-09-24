using Carsharing.ViewModels;
using Contracts;
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
    
    [HttpGet("model/{id:int}")]
    public async Task<IActionResult> GetCarModelByTariff([FromRoute] int id)
    {
        try
        {
            var model = await _carService.GetModelByIdAsync(id);
            return new JsonResult(new ExpandedCarModelVM
            {
                Brand = model.Brand,
                Description = model.Description,
                Model = model.Model,
                Url = model.ImageUrl,
                TariffId = model.TariffId,
                Id = model.Id,
                Price = model.Price,
                MaxMilage = model.Restrictions,
                TariffName = model.TariffName
            });
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpGet("available")]
    public async Task<IActionResult> GetFreeCars([FromQuery] FindCarsVM carSearch)
    {
        var cars = await _carService.GetAvailableCarsByLocationAsync(new SearchCarDto()
        {
            Latitude = carSearch.Latitude,
            Longitude = carSearch.Longitude,
            Radius = carSearch.Radius,
            CarModelId = carSearch.CarModelId
        });
        
        return new JsonResult(cars.Select(x => new CarVM
        {
            Id = x.CarId,
            ParkingLatitude = x.Location!.Latitude,
            ParkingLongitude = x.Location!.Longitude,
            LicensePlate = x.Plate
        }));
    }
}