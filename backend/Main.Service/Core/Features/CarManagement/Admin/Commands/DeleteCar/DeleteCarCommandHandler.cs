using Domain.Repository;
using Entities.Repository;

namespace Features.CarManagement.Admin.Commands.DeleteCar;

public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly ISubscriptionRepository _subscriptionReposiotry;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCarCommandHandler(ICarRepository carRepository, ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _carRepository = carRepository;
        _subscriptionReposiotry = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetByIdAsync(request.Id);

        if (car == null)
            return new Error("Car was not found.");

        var activeSubs = await _subscriptionReposiotry.GetSubscriptionsByCarIdAsync(car!.Id);
        foreach(var sub in activeSubs)
        {
            sub.CarId = null;
            sub.IsActive = false;

            await _subscriptionReposiotry.UpdateAsync(sub);
        }

        await _carRepository.RemoveAsync(car);

        await _unitOfWork.SaveChangesAsync();

        return Result.SuccessResult;
    }
}