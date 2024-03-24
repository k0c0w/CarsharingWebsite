using Domain.Repository;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Admin.Commands.SetCarHasToBeNonActive;

public class SetCarHasToBeNonActiveCommandHandler : ICommandHandler<SetCarHasToBeNonActiveCommand>
{
    private readonly IUnitOfWork<ICarRepository> _carRepository;

    public SetCarHasToBeNonActiveCommandHandler(IUnitOfWork<ICarRepository> carRepository) => _carRepository = carRepository;

    public async Task<Result> Handle(SetCarHasToBeNonActiveCommand request, CancellationToken cancellationToken)
    {
        var requestedCar = await _carRepository.Unit.GetByIdAsync(request.Id);

        if (requestedCar == null)
            return new Error("Car was not found.");

        requestedCar.HasToBeNonActive = true;
        await _carRepository.Unit.UpdateAsync(requestedCar);

        await _carRepository.SaveChangesAsync();

        return Result.SuccessResult;
    }
}