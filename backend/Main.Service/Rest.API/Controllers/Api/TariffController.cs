using Carsharing.Helpers.Extensions.Controllers;
using Features.Tariffs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Controllers;

[Route("api/tariffs")]
[ApiController]
public class TariffController : ControllerBase
{
    private readonly ISender _mediatr;

    public TariffController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet]
    public async Task<IActionResult> Tariff()
    {
        var tariffsResult = await _mediatr.Send(new GetActiveTariffsQuery());
        if (!tariffsResult)
            return this.BadRequestWithErrorMessage(tariffsResult.ErrorMessage);

        return new JsonResult(tariffsResult.Value!.Select(x => new
        { id = x.Id, name = x.Name, price = x.PriceInRubles, description = x.Description }));
    }

    [HttpGet("{tariffId:int}")]
    public async Task<IActionResult> GetTariffById([FromRoute] int tariffId)
    {
        if (tariffId <= 0)
            return NotFound();

        var tariffResult = await _mediatr.Send(new GetActiveTariffsQuery(tariffId));

        if (!tariffResult)
            return NotFound();

        var tariff = tariffResult.Value!.First();

        return new JsonResult(new
        {
            id = tariff.Id,
            name = tariff.Name,
            price = tariff.PriceInRubles,
            description = tariff.Description,
            image_url = tariff.Image
        });
    }
}