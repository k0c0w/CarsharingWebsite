using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.OpenCar;

public class OpenCarCommandHandler : ICommandHandler<OpenCarCommand>
{
    private readonly CarsharingContext _ctx;

    public OpenCarCommandHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Result> Handle(OpenCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _ctx.Cars.FindAsync(request.CarId);
        if (car is null)
            return new Error(new NullReferenceException().Message);
        try
        {
            car.IsOpened = true;
            await _ctx.SaveChangesAsync(cancellationToken);
            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }
}