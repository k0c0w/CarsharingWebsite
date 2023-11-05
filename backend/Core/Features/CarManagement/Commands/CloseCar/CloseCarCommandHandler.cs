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
        var car = await _ctx.Cars.FindAsync(request.CarId);
        if (car is null)
            return new Error("Машина не найдена");
        try
        {
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