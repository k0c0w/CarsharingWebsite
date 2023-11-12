using Domain.Entities;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
namespace Features.CarManagement.Admin.Commands.CreateModel;

public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, int>
{
    private readonly CarsharingContext _ctx;

    public CreateModelCommandHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
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

        //todo: save photo
        model.ImageUrl = "link";

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok<int>(model.Id);
    }
}