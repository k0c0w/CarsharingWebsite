using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.SetCarTaken;

public class SetCarTakenCommandHandler : ICommandHandler<SetCarTakenCommand>
{
    private readonly CarsharingContext _ctx;

    public SetCarTakenCommandHandler(CarsharingContext ctx) => _ctx = ctx;

    public async Task<Result> Handle(SetCarTakenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var requestedCar = await _ctx.Cars.FindAsync(request.Id);
            if (requestedCar == null || requestedCar.IsTaken || requestedCar.HasToBeNonActive) 
                return new Error();
            requestedCar.IsTaken = true;
            await _ctx.SaveChangesAsync();
            return new Ok();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            //логировать что машина уже занята?
            return new Error(exception.Message);
        }
    }
}