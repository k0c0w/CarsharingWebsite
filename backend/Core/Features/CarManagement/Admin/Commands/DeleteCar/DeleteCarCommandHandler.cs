using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Admin.Commands.DeleteCar;

public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand>
{
    private readonly CarsharingContext _ctx;

    public DeleteCarCommandHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Result> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _ctx.Cars.FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        var bindSubs = await _ctx.Subscriptions.Where(x => x.CarId == request.Id)
            .ToListAsync(cancellationToken: cancellationToken);

        _ctx.Subscriptions.UpdateRange(bindSubs.Select(x =>
        {
            x.CarId = null;
            return x;
        }));

        _ctx.Cars.Remove(car);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok();
    }
}