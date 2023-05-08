using Carsharing.ViewModels.Admin;
using Contracts.Tariff;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Admin;
using Services.Exceptions;

namespace Carsharing.Controllers;

[ApiController]
[Route("admin/api/tariff")]
public class AdminTariffController : ControllerBase
{
    private readonly IAdminTariffService _service;

    public AdminTariffController(IAdminTariffService service)
    {
        _service = service;
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
            return new BadRequestObjectResult(new { error = "Such tariff exists" });
        }

        return new CreatedResult("/tariffs", null);
    }
}