using Microsoft.Extensions.FileProviders;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.ReleaseCar;

public class ReleaseCarCommandHandler: ICommandHandler<ReleaseCarCommand>
{
    private readonly CarsharingContext _ctx;

    public ReleaseCarCommandHandler(CarsharingContext ctx) => _ctx = ctx;
    
    public async Task<Result> Handle(ReleaseCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _ctx.Cars.FindAsync(request.CarId);
        if (car != null)
        {
            car.IsOpened = false;
            await _ctx.SaveChangesAsync();
        }
        
        return Result.SuccessResult; 
    }
}