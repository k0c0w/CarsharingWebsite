using Carsharing.ViewModels;
using Contracts;
using Features.CarManagement.Queries.GetModelById;
using Features.CarManagement.Queries.GetModelsByTariffId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Exceptions;

namespace Carsharing.Controllers;


[Route("api/cars")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IMediator _mediator;

    public CarController(ICarService service, IMediator mediator)
    {
        _carService = service;
        _mediator = mediator;
    }
    
    [HttpGet("models/{tariff:int}")]
    public async Task<IActionResult> GetCarModelsByTariff([FromRoute] int tariff)
    {
        //TODO: Проверить cqrs
        var models = ( await _mediator.Send(new GetModelsByTariffIdQuery(tariff)) ).Value;
        // var models = await _carService.GetModelsByTariffIdAsync(tariff);

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
            //TODO: Проверить cqrs
            var result = await _mediator.Send(new GetModelByIdQuery(id));
            if (!result.IsSuccess) 
                throw new Exception(result.ErrorMessage);

            var model = result.Value;
            
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