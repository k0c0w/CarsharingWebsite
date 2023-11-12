using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.CloseCar;

public class CloseCarCommandHandler : ICommandHandler<CloseCarCommand>
{
    private readonly CarsharingContext _ctx;

    public CloseCarCommandHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Result> Handle(CloseCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _ctx.Cars.FirstOrDefaultAsync(x => x.LicensePlate == request.LicensePlate);
        if (car is null)
            return new Error("Машина не найдена");
        try
        {
            // todo: check if current user is owner
            car!.IsOpened = false;
            await _ctx.SaveChangesAsync(cancellationToken);
            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }
}