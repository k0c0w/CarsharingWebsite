using Carsharing.ViewModels;
using Contracts;
using Features.CarBooking.Commands.BookCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;

namespace Carsharing.Controllers;

[Route("api/booking")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Добавил в BookingVm userInfoId, для заполнения Dto и в принципе сделал через UserInfoId
    [HttpPost("rent")]
    public async Task<IActionResult> BookCar([FromBody] BookingVM bookingInfo)
    {

        var commandResult = await _mediator.Send(new BookCarCommand(new RentCarDto()
        {
            PotentialRenterUserId = User.GetId(),
            End = bookingInfo.EndDate,
            Start = bookingInfo.StartDate,
            CarId = bookingInfo.CarId,
            TariffId = bookingInfo.TariffId
        }));
        return commandResult
            ? new JsonResult(new
            {
                result = "Car is successfuly booked"
            })
            : new JsonResult(new
            {
                result = commandResult.ErrorMessage
            });

        // catch (ObjectNotFoundException)
        // {
        //     return BadRequest(new { error = "Не возможно забронировать. Некоторые аргументы не действительны." });
        // }
        // catch (InvalidOperationException ex)
        // {
        //     return BadRequest(new { error = "Не возможно забронировать", type = $"{ex.Message}" });
        // }
        // catch (CarAlreadyBookedException)
        // {
        //     return BadRequest(new { error = "Машина уже забронированна." });
        // }
    }
}