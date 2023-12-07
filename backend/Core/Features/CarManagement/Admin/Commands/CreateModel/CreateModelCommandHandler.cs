using Clients.S3ServiceClient;
using Domain.Entities;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
namespace Features.CarManagement.Admin.Commands.CreateModel;

public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, int>
{
    private readonly CarsharingContext _ctx;
    private readonly S3ServiceClient _s3Service;

    public CreateModelCommandHandler(CarsharingContext ctx, IHttpClientFactory factory)
    {
        _ctx = ctx;
        _s3Service = new S3ServiceClient(factory.CreateClient("authorized"));
    }

    public async Task<Result<int>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {


        var photo = request.ModelPhoto;
        var creationResult = await _s3Service.CreateFileAsync(photo.Name, photo.Content, photo.ContentType);

        if (!creationResult.Success)
            return new Error<int>("Failed to save picture.");

        var model = new CarModel()
        {
            Brand = request.Brand!,
            Model = request.Model!,
            Description = request.Description!,
            TariffId = request.TariffId,
            ImageUrl = creationResult.Data.Url,
        };

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok<int>(model.Id);
    }
}