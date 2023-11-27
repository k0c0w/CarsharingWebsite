using Contracts;
using Domain.Common;
using Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Configuration;
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
            ImageUrl = $"http://localhost:7126/api/files/models/{request.Brand}_{request.Model}"
        };

        var memorystream = new MemoryStream();
        request.ModelPhoto.Content.CopyTo(memorystream);
        

        await _fileProducer.SendFileAsync(new SaveImageDto() {
            Name = $"{request.Brand}_{request.Model}",
            Image = memorystream.ToArray()
        });

        await _ctx.CarModels.AddAsync(model, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);

        return new Ok<int>(model.Id);
    }
}