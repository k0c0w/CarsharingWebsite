using AutoMapper;
using Contracts;
using Domain.Entities;
using Migrations.CarsharingApp;
using Services.Abstractions;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;
using File = Contracts.File;

namespace Features.CarManagement.Commands.EditModel;

public class EditModelCommandHandler : ICommandHandler<EditModelCommand>
{
    private readonly CarsharingContext _ctx;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public EditModelCommandHandler(CarsharingContext ctx, IMapper mapper, IFileProvider fileProvider)
    {
        _ctx = ctx;
        _mapper = mapper;
        _fileProvider = fileProvider;
    }

    public async Task<Result> Handle(EditModelCommand request, CancellationToken cancellationToken)
    {
        var old = await _ctx.CarModels.FindAsync(request.EditCarModelDto);
        if (old == null)
            return new Error($"{new ObjectNotFoundException(nameof(CarModel)).Message}");

        if (request.EditCarModelDto.Description != null)
            old.Description = request.EditCarModelDto.Description;
        if (request.EditCarModelDto.Image != null)
            await UpdateModelImage(old, request.EditCarModelDto.Image);
        
        _ctx.CarModels.Update(old);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok();
    }
    
    private async Task UpdateModelImage(CarModel old, IFile file)
    {
        var folder = Path.Combine("wwwroot", "models");

        _fileProvider.Delete(folder, old.ImageName);

        await _fileProvider.SaveAsync(folder, new File {Content = file.Content, Name = old.ImageName});
    }
}