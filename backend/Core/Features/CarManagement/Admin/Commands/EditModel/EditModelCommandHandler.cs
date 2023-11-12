using Contracts;
using Domain.Entities;
using Migrations.CarsharingApp;
using Services.Abstractions;
using Shared.CQRS;
using Shared.Results;
using File = Contracts.File;

namespace Features.CarManagement.Admin.Commands.EditModel;

public class EditModelCommandHandler : ICommandHandler<EditModelCommand>
{
    private readonly CarsharingContext _ctx;
    private readonly IFileProvider _fileProvider;

    public EditModelCommandHandler(CarsharingContext ctx, IFileProvider fileProvider)
    {
        _ctx = ctx;
        _fileProvider = fileProvider;
    }

    public async Task<Result> Handle(EditModelCommand request, CancellationToken cancellationToken)
    {
        var old = await _ctx.CarModels.FindAsync(request.ModelId);
        if (old == null)
            return new Error("ObjectNotFound");

        if (request.Description != null)
            old.Description = request.Description;
        if (request.Image != null)
            await UpdateModelImage(old, request.Image);

        _ctx.CarModels.Update(old);
        await _ctx.SaveChangesAsync(cancellationToken);

        return Result.SuccessResult;
    }

    private async Task UpdateModelImage(CarModel old, IFile file)
    {
        var folder = Path.Combine("wwwroot", "models");

        _fileProvider.Delete(folder, old.ImageName);

        await _fileProvider.SaveAsync(folder, new File { Content = file.Content, Name = old.ImageName });
    }
}