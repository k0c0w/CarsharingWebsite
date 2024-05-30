using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Features.CarManagement.Admin.Commands.TryDeleteModel;

public class TryDeleteModelCommandHandler : ICommandHandler<DeleteModelCommand>
{
    private readonly CarsharingContext _ctx;

    public TryDeleteModelCommandHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Result> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        if (await _ctx.Cars.AnyAsync(x => x.CarModelId == request.Id, cancellationToken: cancellationToken))
            return new Error($"{new InvalidOperationException().Message}");
        var model = await _ctx.CarModels.FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        _ctx.CarModels.Remove(model);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok();
    }
}