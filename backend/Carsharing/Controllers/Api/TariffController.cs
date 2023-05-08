using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

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
}