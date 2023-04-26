using Carsharing.ViewModels;
using Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Carsharing.Controllers;

[Area("Api")]
public class BookingController : Controller
{
    private readonly BookingService _service;
    
    public BookingController(BookingService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Re()
    {
        return Ok("dfdfd");
    }
    
    [HttpPost]
    public async Task<IActionResult> BookCar([FromBody] BookingVM bookingInfo)
    {
        try
        {
            await _service.BookCarAsync(new RentCarDto
            {
                PotentialRenterUserId = 3,
                TariffId = bookingInfo.TariffId,
                CarId = bookingInfo.CarId,
                Start = bookingInfo.StartDate,
                End = bookingInfo.EndDate
            });
            return Ok("Ура");
        }
        catch
        {
            return BadRequest("Не возможно забронировать");
        }
    }
}