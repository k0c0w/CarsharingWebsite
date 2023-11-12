using Domain.Entities;
using Features.CarManagement;
using MediatR;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarBooking.Commands.AssignCarToUser;

public class AssignCarToUserCommandHandler : ICommandHandler<AssignCarToUserCommand>
{
    private readonly IMediator _mediator;
    private readonly CarsharingContext _ctx;

    public AssignCarToUserCommandHandler(IMediator mediator, CarsharingContext ctx)
    {
        _mediator = mediator;
        _ctx = ctx;
    }

    public async Task<Result> Handle(AssignCarToUserCommand request, CancellationToken cancellationToken)
    { 
        var isAssigned = await _mediator.Send(new SetCarTakenCommand(request.Details.CarId), cancellationToken); 
        
        if(!isAssigned) 
            return new Error(new CarAlreadyBookedException().Message);
        request.UserInfo.Balance -= request.Withdrawal;
        // сейчас аренда машины доступна только с текущего дня, без планирования
        var sub = new Subscription
        {
            StartDate = request.Details.Start, 
            EndDate = request.Details.End, 
            IsActive = true, 
            UserId = request.UserInfo.UserId,
            CarId = request.Details.CarId
        };
        _ctx.Subscriptions.Add(sub);
        try
        {
            await _ctx.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            //TODO: Проверить корректность 
            await _mediator.Send(new ReleaseCarCommand(request.Details.CarId), cancellationToken);
        }

        return Result.SuccessResult;
    }
}