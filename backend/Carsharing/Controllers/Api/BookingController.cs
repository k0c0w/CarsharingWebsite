using Carsharing.ViewModels;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstractions;
using Services.Exceptions;

namespace Carsharing.Controllers;

[Route("api/booking")]
public class BookingController : Controller
{
    private readonly IBookingService _service;
    
    public BookingController( IBookingService service)
    {
        _service = service;
    }
    
    //Добавил в BookingVm userInfoId, для заполнения Dto и в принципе сделал через UserInfoId
    [HttpPost("rent")]
    public async Task<IActionResult> BookCar([FromBody] BookingVM bookingInfo)
    {
        try
        {
            await _service.BookCarAsync(new RentCarDto()
            {
                PotentialRenterUserInfoId = bookingInfo.UserInfoId,
                End = bookingInfo.EndDate,
                Start = bookingInfo.StartDate,
                CarId = bookingInfo.CarId,
                TariffId = bookingInfo.TariffId,
            });
            return new JsonResult(new
            {
                result = "Car is successfuly booked"
            });
        }
        catch (ObjectNotFoundException)
        {
            return BadRequest(new { error = "Не возможно забронировать. Некоторые аргументы не действительны." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = "Не возможно забронировать", type = $"{ex.Message}" });
        }
        catch (CarAlreadyBookedException)
        {
            return BadRequest(new { error = "Машина уже забронированна." });
        }
    }
}