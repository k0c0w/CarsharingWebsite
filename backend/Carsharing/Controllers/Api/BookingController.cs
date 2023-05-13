using Carsharing.ViewModels;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Exceptions;

namespace Carsharing.Controllers;

[Area("Api")]
public class BookingController : Controller
{
    private readonly BookingService _service;
    
    public BookingController(BookingService service)
    {
        _service = service;
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
            return Ok(new {success=true, booked_car_id=bookingInfo.CarId});
        }
        catch (ObjectNotFoundException)
        {
            return BadRequest(new { error = "Не возможно забронировать. Некоторые аргументы не действительны." });
        }
        catch (InvalidOperationException)
        {
            return BadRequest(new { error = "Не возможно забронировать" });
        }
        catch (CarAlreadyBookedException)
        {
            return BadRequest(new { error = "Машина уже забронированна." });
        }
    }
}