using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Admin.Commands.CreateCar;

public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, int>
{
    private readonly CarsharingContext _ctx;

    public CreateCarCommandHandler(CarsharingContext ctx, IFileProducer fileProducer)
    {
        _ctx = ctx;
    }

    public async Task<Result<int>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = new Car()
        {
            CarModelId = request.CarModelId,
            LicensePlate = request.LicensePlate,

            ParkingLatitude = request.ParkingLatitude,

            ParkingLongitude = request.ParkingLongitude,
        };

        await _ctx.Cars.AddAsync(car, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);
        return new Ok<int>(car.Id);
    }
}