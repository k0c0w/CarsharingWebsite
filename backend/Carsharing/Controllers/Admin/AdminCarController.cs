using AutoMapper;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Admin.Car;
using Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Admin;
using System.Text.Json;
using Carsharing.Helpers;

namespace Carsharing.Controllers;

[Route("api/admin/car")]
[ApiController]
public class AdminCarController : ControllerBase
{
    private readonly IAdminCarService _carService;
    private readonly IMapper _mapper;
    public AdminCarController(IAdminCarService carService, IMapper mapper)
    {
        _mapper = mapper;
        _carService = carService;
    }

    [HttpGet("models")]
    public async Task<IActionResult> GetCarModels()
    {
        var models = await _carService.GetAllModelsAsync();
        return new JsonResult(models.Select(x => new CarModelVM
        {
            Id = x.Id,
            Brand = x.Brand,
            Description = x.Description,
            Model = x.Model,
            TariffId = x.TariffId,
            Url = x.ImageUrl
        }));
    }


    [Consumes("multipart/form-data")]
    [HttpPost("model/create")]
    public async Task<IActionResult> CreateCarModel([FromForm] CreateCarModelVM create)
    {
        try
        {
            await _carService.CreateModelAsync(_mapper.Map<CreateCarModelDto>(create));
            return Created("models", null);
        }
        catch (ObjectDisposedException)
        {
            return new JsonResult(new {error=new {code=(int)ErrorCode.ServiceError, messages=new [] {"Модель создана, но фотография не сохранилась."}}});
        }
    }


    [HttpPut("model/{id:int}")]
    public async Task<IActionResult> UpdateCarModelInfo([FromRoute] int id, [FromBody] EditCarModelVM edit)
    {
        if (id <= 0) return NotFound();
        
        Contracts.File? file = null;
        if (edit.Image != null)
            file = new Contracts.File()
            {
                Name = edit.Image.FileName,
                Content = edit.Image.OpenReadStream()
            };

        await _carService.EditModelAsync(id, new EditCarModelDto
        {
            Description = edit.Description,
            Image = file
        });

        return NoContent();
    }

    [HttpDelete("model/{id:int}")]
    public async  Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _carService.TryDeleteModelAsync(id);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("models")]
    public async Task<IActionResult> DeleteRange([FromBody] IEnumerable<int> modelsId)
    {
        try
        {

            return NoContent();
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("cars")]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        return new JsonResult(MapToAdminCarVm(cars));
    }
    
    [HttpGet("cars/{modelId:int}")]
    public async Task<IActionResult> GetAllCarsByModel([FromRoute] int modelId)
    {
        var cars = await _carService.GetCarsByModelAsync(modelId);
        return new JsonResult(MapToAdminCarVm(cars));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarVM create)
    {
        await _carService.CreateCarAsync(new CreateCarDto
        {
            LicensePlate = create.LicensePlate,
            ParkingLatitude = create.ParkingLatitude,
            ParkingLongitude = create.ParkingLongitude,
            CarModelId = create.CarModelId
        });
        return Created("cars", null);
    }
    

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCar([FromRoute] int id)
    {
        if (id <= 0) return NotFound();
        try
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    private IEnumerable<AdminCarVM> MapToAdminCarVm(IEnumerable<CarDto> cars)
        => cars.Select(x => new AdminCarVM
        {
            Id = x.Id,
            IsOpened = x.IsOpened,
            IsTaken = x.IsTaken,
            LicensePlate = x.LicensePlate,
            ParkingLatitude = x.ParkingLatitude,
            ParkingLongitude = x.ParkingLongitude,
            CarModelId = x.CarModelId,
            HasToBeNonActive = x.HasToBeNonActive
        });
}