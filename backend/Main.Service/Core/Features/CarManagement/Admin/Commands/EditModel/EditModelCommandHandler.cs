using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
namespace Features.CarManagement.Admin.Commands.EditModel;

public class EditModelCommandHandler : ICommandHandler<EditModelCommand>
{
    private readonly CarsharingContext _ctx;

    public EditModelCommandHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Result> Handle(EditModelCommand request, CancellationToken cancellationToken)
    {
        var old = await _ctx.CarModels.FirstOrDefaultAsync(x => x.Id == request.ModelId, cancellationToken);
        if (old == null)
            return new Error("ObjectNotFound");

        if (request.Description != null)
            old.Description = request.Description;
        if (request.Image != null)
           UpdateModelImage(old);

        _ctx.CarModels.Update(old);
        await _ctx.SaveChangesAsync(cancellationToken);

        return Result.SuccessResult;
    }

    private static void UpdateModelImage(CarModel old)
    {
        //todo: resave images
        old.ImageUrl = "link";
    }
}