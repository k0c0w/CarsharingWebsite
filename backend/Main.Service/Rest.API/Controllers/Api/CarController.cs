 using Carsharing.ViewModels;
using Features.CarManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Controllers;


[Route("api/cars")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("models/{tariff:int}")]
    public async Task<IActionResult> GetCarModelsByTariff([FromRoute] int tariff)
    {
        var models = (await _mediator.Send(new GetModelsByTariffIdQuery(tariff))).Value;

        if (models == null || !models.Any()) return NotFound();
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
        var result = await _mediator.Send(new GetModelByIdQuery(id));
        if (!result) 
            return BadRequest(result.ErrorMessage);

        var model = result.Value;
        
        return new JsonResult(new ExpandedCarModelVM
        {
            Brand = model!.Brand,
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
    
    [HttpGet("available")]
    public async Task<IActionResult> GetFreeCars([FromQuery] FindCarsVM carSearch)
    {
        var carsResult = await _mediator.Send(new GetAvailableCarsByLocationQuery()
        {
            Latitude = carSearch.Latitude,
            Longitude = carSearch.Longitude,
            Radius = carSearch.Radius,
            CarModelId = carSearch.CarModelId
        });


        if (!carsResult)
            return BadRequest();

        return new JsonResult(carsResult.Value!.Select(x => new CarVM
        {
            Id = x.CarId,
            ParkingLatitude = x.Location!.Latitude,
            ParkingLongitude = x.Location!.Longitude,
            LicensePlate = x.Plate
        }));
    }
}