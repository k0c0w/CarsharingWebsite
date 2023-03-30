using Carsharing.Forms;
using Carsharing.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarModelController : Controller
{
    private readonly CarsharingContext _carsharingContext;

    public CarModelController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]CarModelDto dto)
    {
        var id = await _carsharingContext.CarModels.CountAsync() + 1;
        var newCarModel = new CarModel
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            SourceImg = dto.SourceImg,
            TarrifId = dto.TariffId
        };
        await _carsharingContext.AddAsync(newCarModel);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }
}