using AutoMapper;
using Domain.Entities;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
using Services.Abstractions;

namespace Features.CarManagement.Commands.CreateModel;

public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;
    private readonly IFileProvider _fileProvider;

    public CreateModelCommandHandler(CarsharingContext ctx, IMapper mapper, IFileProvider fileProvider)
    {
        _ctx = ctx;
        _mapper = mapper;
        _fileProvider = fileProvider;
    }

    public async Task<Result> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<CarModel>(request.CreateCarModelDto);

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        var photo = request.CreateCarModelDto.ModelPhoto! with { Name = model.ImageName };
        await _fileProvider.SaveAsync(Path.Combine("wwwroot", "models"), photo);

        return new Ok();
    }
}