using Domain.Repository;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.ReleaseCar;

public class ReleaseCarCommandHandler: ICommandHandler<ReleaseCarCommand>
{
    private readonly IUnitOfWork<ICarRepository> _carRepository;

    public ReleaseCarCommandHandler(IUnitOfWork<ICarRepository> carRepository) => _carRepository = carRepository;
    
    public async Task<Result> Handle(ReleaseCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.Unit.GetByIdAsync(request.CarId);
        if (car != null)
        {
            car.IsOpened = false;
            car.IsTaken = false;
            car.Prebooked = false;

            await _carRepository.SaveChangesAsync();
        }
        
        return Result.SuccessResult; 
    }
}