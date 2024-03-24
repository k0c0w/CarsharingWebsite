using Clients.S3ServiceClient;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
namespace Features.CarManagement.Admin.Commands.CreateModel;

public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, int>
{
    private readonly CarsharingContext _ctx;
    private readonly S3ServiceClient _s3Service;
    private readonly ILogger<string> _logger;

    public CreateModelCommandHandler(CarsharingContext ctx, IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<string> logger)
    {
        _logger = logger;
        _ctx = ctx;
        _s3Service = new S3ServiceClient(configuration["KnownHosts:BackendHosts:FileService"], clientFactory.CreateClient("authorized"));
    }

    public async Task<Result<int>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var photo = request.ModelPhoto;
        var creationResult = await _s3Service.CreateFileAsync(photo.Name, "models", photo.Content, photo.ContentType);

        if (!creationResult.Success)
        {
            _logger.LogInformation(creationResult.Error.Message);
            return new Error<int>("Failed to save picture.");
        }

        var model = new CarModel()
        {
            Brand = request.Brand!,
            Model = request.Model!,
            Description = request.Description!,
            TariffId = request.TariffId,
            //todo: убрать костыльный реплейс
            ImageUrl = creationResult.Data.Url.Replace("host.docker.internal", "localhost"),
        };

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok<int>(model.Id);
    }
}