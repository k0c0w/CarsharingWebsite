using AutoMapper;
using Carsharing.Helpers;
using Carsharing.Helpers.Extensions.Controllers;
using Carsharing.ViewModels.Admin;
using Features.Tariffs.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Carsharing.Controllers;

[ApiController]
[Route("api/admin/tariff")]
[Authorize(Roles = "Admin")]
public class AdminTariffController : ControllerBase
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public AdminTariffController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTariffs()
    {
        var tariffsResult = await _mediatr.Send(new GetTariffsQuery());

        if (!tariffsResult)
            return StatusCode((int)HttpStatusCode.InternalServerError, tariffsResult.ErrorMessage);

        var tariffsVM = _mapper.Map<IEnumerable<TariffVM>>(tariffsResult.Value);
        return new JsonResult(tariffsVM);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateTariffVM vm)
    {
        var createTariffResult = await _mediatr.Send(new CreateTariffCommand(vm.Name, vm.Price, vm.Description, vm.MaxMillage));

        if (!createTariffResult.IsSuccess)
            return this.BadRequestWithErrorMessage(createTariffResult.ErrorMessage);

        return Created("/tariffs", null);
    }

    [HttpPut("setstate/{id:int}")]
    public async Task<IActionResult> SwitchTariffState([FromRoute] int id, [FromBody] bool state)
    {
        var command = state ? new TurnOnTariffCommand(id) : new TurnOnTariffCommand(id);

        var switchStateResult = await _mediatr.Send(command);

        if (!switchStateResult)
            return this.BadRequestWithErrorMessage(switchStateResult.ErrorMessage);

        return NoContent();
    }

    [HttpPut("edit/{id:int}")]
    public async Task<IActionResult> EditTariff([FromRoute] int id, [FromBody] CreateTariffVM edit)
    {
        var updateResult = await _mediatr.Send(new UpdateTariffCommand(id, edit.Name, edit.Description, edit.Price, edit.MaxMillage));
        
        if (updateResult)
            return NoContent();

        return this.BadRequestWithErrorMessage(updateResult.ErrorMessage);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteTariff([FromRoute] int id)
    {
        var deleteResult = await _mediatr.Send(new DeleteTariffCommand(id));

        if (deleteResult)
            return NoContent();

        return this.BadRequestWithErrorMessage(deleteResult.ErrorMessage);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTariff([FromRoute] int id)
    {
        var getTariffResult = await _mediatr.Send(new GetTariffsQuery(id));

        if (getTariffResult)
            return new JsonResult(_mapper.Map<TariffVM>(getTariffResult.Value));

        return this.BadRequestWithErrorMessage(getTariffResult.ErrorMessage);
    }

    private IActionResult GetInvalidIdResponse() => BadRequest(new { error = new { code = (int) ErrorCode.ViewModelError, messages = new[] { "Invalid id value" } } });
}