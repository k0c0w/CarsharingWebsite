using Domain.Repository;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.CloseCar;

public class CloseCarCommandHandler : ICommandHandler<CloseCarCommand>
{
    private readonly IUnitOfWork<ICarRepository> _carRepository;

    public CloseCarCommandHandler(IUnitOfWork<ICarRepository> carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Result> Handle(CloseCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.Unit.GetByLiciensePlateAsync(request.LicensePlate);
        if (car is null)
            return new Error("Машина не найдена");

        // todo: check if current user is owner
        car!.IsOpened = false;
        await _carRepository.Unit.UpdateAsync(car);
        await _carRepository.SaveChangesAsync();

        return Result.SuccessResult;
    }
}