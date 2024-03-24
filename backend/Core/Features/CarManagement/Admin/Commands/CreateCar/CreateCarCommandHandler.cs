using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;
using System.Diagnostics;

namespace Features.CarManagement.Admin.Commands.CreateCar;

public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, int>
{
    private readonly IUnitOfWork<ICarRepository> _carRepository;

    public CreateCarCommandHandler(IUnitOfWork<ICarRepository> carRepository)
    {
        _carRepository = carRepository;
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

        await _carRepository.Unit.AddAsync(car);
        await _carRepository.SaveChangesAsync();

        Debug.Assert(car.Id != default);

        return new Ok<int>(car.Id);
    }
}