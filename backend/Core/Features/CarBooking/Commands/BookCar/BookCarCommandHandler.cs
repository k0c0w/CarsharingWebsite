using Features.CarBooking.Commands.AssignCarToUser;
using Features.CarBooking.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarBooking.Commands.BookCar;

public class BookCarCommandHandler : ICommandHandler<BookCarCommand>
{
    private readonly CarsharingContext _ctx;
    private readonly IMediator _mediator;

    public BookCarCommandHandler(CarsharingContext ctx, IMediator mediator)
    {
        _ctx = ctx;
        _mediator = mediator;
    }

    public async Task<Result> Handle(BookCarCommand request, CancellationToken cancellationToken)
    {
        if (request.RentCarInfo.Start > request.RentCarInfo.End || request.RentCarInfo.Days == 0) 
            return new Error("Wrong date bounds");
        
        var carSupportsTariff = await _ctx.Cars
            .Include(x => x.CarModel)
            .ThenInclude(x => x!.Tariff)
            .Where(x => x.Id == request.RentCarInfo.CarId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (carSupportsTariff == null)
            throw new ObjectNotFoundException(
                $"car({request.RentCarInfo.CarId}) is not associated with tariff({request.RentCarInfo.TariffId})");
        
        var tariff = carSupportsTariff.CarModel!.Tariff!;
        if (request.RentCarInfo!.PotentialRenterUserId == null)
            return new Error(new ObjectNotFoundException("").Message);

        var result = await _mediator.Send(new GetConfirmedUserInfoQuery(request.RentCarInfo!.PotentialRenterUserId));
        if (!result.IsSuccess || result.Value is null)
            return new Error();
        var userInfo = result.Value;
        var total = tariff!.Price * request.RentCarInfo.Days;
        
        if (userInfo.Balance < total) 
            return new Error(new InvalidOperationException("Not enough money to book car").Message);
        
        await _mediator.Send(new AssignCarToUserCommand(userInfo, request.RentCarInfo, tariff.Price), cancellationToken);

        return Result.SuccessResult;
    }
}