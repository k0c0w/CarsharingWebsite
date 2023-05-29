using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Exceptions;

namespace Carsharing.Controllers;

[Route("api/tariffs")]
[ApiController]
public class TariffController : ControllerBase
{
    private readonly ITariffService _tariffService;

    public TariffController(ITariffService service)
    {
        _tariffService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Tariff()
    {
        var tariffs = await _tariffService.GetAllActiveAsync();
        return new JsonResult(tariffs.Select(x => new
            { id = x.Id, name = x.Name, price = x.PriceInRubles, description = x.Description }));
    }

    [HttpGet("{tariffId:int}")]
    public async Task<IActionResult> GetTariffById([FromRoute] int tariffId)
    {
        try
        {
            var tariff = await _tariffService.GetActiveTariffById(tariffId);
            return new JsonResult(new
            {
                id = tariff.Id, name = tariff.Name, price = tariff.PriceInRubles, description = tariff.Description,
                image_url=tariff.Image
            });
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }
}