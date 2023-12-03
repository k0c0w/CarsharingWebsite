using AutoMapper;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Admin.Car;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Carsharing.Helpers.Extensions.Controllers;
using MediatR;
using Features.CarManagement;
using Features.CarManagement.Admin;
using System.Net.Mime;

namespace Carsharing.Controllers;

[Route("api/admin/car")]
[ApiController]
public class AdminCarController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AdminCarController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("models")]
    public async Task<IActionResult> GetCarModels()
    {

        var modelsResult = await _mediator.Send(new GetAllModelsQuery());

        if (!modelsResult)
            return this.BadRequestWithErrorMessage(modelsResult.ErrorMessage);

        return new JsonResult(modelsResult.Value!.Select(x => new CarModelVM
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
        var modelCreateResult = await _mediator.Send(new CreateModelCommand()
        {
            Brand = create.Brand,
            Description = create.Description,
            Model = create.Model,
            TariffId = create.TariffId,
            ModelPhoto = IFormFileToStream(create!.Image!),
        });

        if (modelCreateResult)
            return Created("models", modelCreateResult.Value);

        return new JsonResult(this.GenerateServiceError(modelCreateResult.ErrorMessage));
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

        var editModelResult = await _mediator.Send(new EditModelCommand()
        {
            ModelId = id,
            Description = edit.Description,
            Image = file
        });

        if(editModelResult)
            return NoContent();

        return this.BadRequestWithErrorMessage(editModelResult.ErrorMessage);
    }

    [HttpDelete("model/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleteResult = await _mediator.Send(new DeleteModelCommand(id));

        return deleteResult ? NoContent() : this.BadRequestWithErrorMessage(deleteResult.ErrorMessage);
    }

    [HttpGet("cars")]
    public async Task<IActionResult> GetAllCars()
    {
        var carsResult = await _mediator.Send(new GetAllCarsQuery());
        return carsResult 
            ? new JsonResult(_mapper.Map<IEnumerable<CarDto>, IEnumerable<AdminCarVM>>(carsResult.Value!)) 
            : this.BadRequestWithErrorMessage(carsResult.ErrorMessage);
    }

    [HttpGet("cars/{modelId:int}")]
    public async Task<IActionResult> GetAllCarsByModel([FromRoute] int modelId)
    {
        var carsByModelResult = await _mediator.Send(new GetCarsByModelQuery(modelId));

        return carsByModelResult 
            ? new JsonResult(_mapper.Map<IEnumerable<CarDto>, IEnumerable<AdminCarVM>>(carsByModelResult.Value!)) 
            : this.BadRequestWithErrorMessage(carsByModelResult.ErrorMessage);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarVM create)
    {
        var createCarResult = await _mediator.Send(new CreateCarCommand()
        {
            CarModelId = create.CarModelId,
            LicensePlate = create.LicensePlate,
            ParkingLatitude = create.ParkingLatitude,
            ParkingLongitude = create.ParkingLongitude
        });

        if (createCarResult.IsSuccess)
            return Created("cars", createCarResult.Value);

        return this.BadRequestWithErrorMessage(createCarResult.ErrorMessage);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCar([FromRoute] int id)
    {
        if (id <= 0) return NotFound();

        var deleteCarResult = await _mediator.Send(new DeleteCarCommand(id));

        return deleteCarResult ? NoContent() : this.BadRequestWithErrorMessage(deleteCarResult.ErrorMessage);
    }

    private static Contracts.File IFormFileToStream(IFormFile formFile)
    {
        Contracts.File file;

        file = new Contracts.File()
        {
            Name = formFile.FileName,
            Content = formFile.OpenReadStream(),
            ContentType = formFile.ContentType,
        };

        return file;
    }
}