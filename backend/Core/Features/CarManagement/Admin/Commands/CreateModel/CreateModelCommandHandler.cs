using AutoMapper;
using Domain.Entities;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
using Services.Abstractions;

namespace Features.CarManagement.Admin.Commands.CreateModel;

public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, int>
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

    public async Task<Result<int>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var model = new CarModel()
        {
            Brand = request.Brand!,
            Model = request.Model!,
            Description = request.Description!,

            TariffId = request.TariffId
        };

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        var photo = request.ModelPhoto! with { Name = model.ImageName };
        await _fileProvider.SaveAsync(Path.Combine("wwwroot", "models"), photo);

        return new Ok<int>(model.Id);
    }
}