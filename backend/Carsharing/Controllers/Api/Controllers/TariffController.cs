using Carsharing.Forms;
using Contracts.Tariff;
using Domain.Entities;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TariffController : ControllerBase
{
    private readonly ITariffService _tariffService;

    public TariffController(ITariffService service)
    {
        _tariffService = service;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Tariffs()
    {
        var tariffs = await _tariffService.GetAllActiveAsync();
        return new JsonResult(tariffs.Select(x => new
            { id = x.Id, name = x.Name, price = x.PriceInRubles, description = x.Description }));
    }
}