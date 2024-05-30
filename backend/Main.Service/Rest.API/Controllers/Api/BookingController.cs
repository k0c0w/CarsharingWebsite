using Carsharing.Helpers;
using Carsharing.ViewModels;
using CommonExtensions.Claims;
using Contracts;
using Features.CarBooking.Commands.BookCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("rent")]
    public async Task<IActionResult> BookCar([FromBody] BookingVM bookingInfo)
    {

        var commandResult = await _mediator.Send(new BookCarCommand(new RentCarDto()
        {
            PotentialRenterUserId = User.GetId(),
            End = bookingInfo.EndDate,
            Start = bookingInfo.StartDate,
            CarId = bookingInfo.CarId,
        }));
        return commandResult
            ? new JsonResult(new
            {
                result = "Car is successfully booked"
            })
            : BadRequest(new
            {
                code = ErrorCode.ServiceError,
                error = commandResult.ErrorMessage
            });
    }
}