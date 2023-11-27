using Contracts;
using Domain.Common;
using Domain.Entities;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
namespace Features.CarManagement.Admin.Commands.CreateModel;

public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, int>
{
    private readonly CarsharingContext _ctx;
    private readonly IFileProducer _fileProducer;
    

    public CreateModelCommandHandler(CarsharingContext ctx, IFileProducer fileProducer)
    {
        _ctx = ctx;
        _fileProducer = fileProducer;
    }

    public async Task<Result<int>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var model = new CarModel()
        {
            Brand = request.Brand!,
            Model = request.Model!,
            Description = request.Description!,
            TariffId = request.TariffId,
            ImageUrl = "no url"
        };

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        var memorystream = new MemoryStream();
        request.ModelPhoto.Content.CopyTo(memorystream);

        await _fileProducer.SendFileAsync(new SaveCarModelImageDto()
        {
            ImageName = $"{request.Brand}_{request.Model}_{(DateTime.Now - DateTime.UnixEpoch).TotalSeconds}",
            Image = memorystream.ToArray(),
            CarModelId = model.Id
        });

        return new Ok<int>(model.Id);
    }
}