using Domain.Repository;
using Entities.Repository;

namespace Features.CarManagement.Commands.OpenCar;

public class OpenCarCommandHandler : ICommandHandler<OpenCarCommand>
{
    private readonly IUnitOfWork<ICarRepository> _carRepository;

    public OpenCarCommandHandler(IUnitOfWork<ICarRepository> carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Result> Handle(OpenCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.Unit.GetByLicensePlateAsync(request.LicensePlate);
        if (car is null)
            return new Error(new NullReferenceException().Message);

        car.IsOpened = true;

        await _carRepository.SaveChangesAsync();
        return Result.SuccessResult;
    }
}