using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Admin.Commands.SetCarHasToBeNonActive;

public class SetCarHasToBeNonActiveCommandHandler : ICommandHandler<SetCarHasToBeNonActiveCommand>
{
    private CarsharingContext _ctx;

    public SetCarHasToBeNonActiveCommandHandler(CarsharingContext ctx) => _ctx = ctx;

    public async Task<Result> Handle(SetCarHasToBeNonActiveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var requestedCar = await _ctx.Cars.FindAsync(request.Id);
            if (requestedCar == null || requestedCar.IsTaken || requestedCar.HasToBeNonActive)
                return new Error();
            requestedCar.HasToBeNonActive = true;
            await _ctx.SaveChangesAsync();
            return new Ok();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            //логировать что машина уже не активна?
            return new Error(exception.Message);
        }
    }
}