using AutoMapper;
using Carsharing.ViewModels.Admin;
using Carsharing.ViewModels.Admin.Car;
using Contracts.Tariff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Admin;
using Services.Exceptions;

namespace Carsharing.Controllers;

[ApiController]
[Route("api/admin/tariff")]
//todo: [Authorize(Roles = "Admin")]
public class AdminTariffController : ControllerBase
{
    private readonly IAdminTariffService _service;
    private readonly IMapper _mapper;

    public AdminTariffController(IAdminTariffService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTariffs()
    {
        var tariffs = await _service.GetAllAsync();
        var tariffsVM = _mapper.Map<IEnumerable<TariffVM>>(tariffs);
        return new JsonResult(tariffsVM);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateTariffVM vm)
    {
        try
        {
            await _service.CreateAsync(new CreateTariffDto()
            {
                Name = vm.Name,
                Description = vm.Description,
                MaxMileage = vm?.MaxMillage,
                PriceInRubles = vm.Price,
            });
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(new { errors = new object[] { new { invalid_arguments = ex.Message } } });
        }
        catch (AlreadyExistsException)
        {
            return BadRequest(new { error = "Such tariff exists" });
        }

        return new CreatedResult("/tariffs", null);
    }

    [HttpPut("setstate/{id:int}")]
    public async Task<IActionResult> SwitchTariffState([FromRoute] int id, [FromBody] bool state)
    {
        try
        {
            if (state)
                await _service.TurnOnAsync(id);
            else
                await _service.TurnOffAsync(id);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut("edit/{id:int}")]
    public async Task<IActionResult> EditTariff([FromRoute] int id, [FromBody] CreateTariffVM edit)
    {
        try
        {
            await _service.EditAsync(id, new CreateTariffDto()
            {
                Description = edit.Description,
                Name = edit.Name,
                MaxMileage = edit.MaxMillage,
                PriceInRubles = edit.Price
            } );
            return NoContent();
        }
        catch (AlreadyExistsException)
        { return BadRequest(new { error = "tariff already exists" }); }
        catch (ArgumentException ex)
        { return BadRequest(new { error = $"invalid arguments:{ex.Message}" }); }
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteTariff([FromRoute] int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new {error=ex.Message});
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTariff([FromRoute] int id)
    {
        try
        {
            var tariff = await _service.GetTariffByIdAsync(id);
            return new JsonResult(_mapper.Map<TariffVM>(tariff));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}