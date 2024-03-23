using AutoMapper;
using Contracts;
using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Admin.Queries.GetAllCars;

public class GetAllCarsQueryHandler : IQueryHandler<GetAllCarsQuery, IEnumerable<CarDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetAllCarsQueryHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarDto>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetBatchAsync();

        var result = _mapper.Map<IEnumerable<Car>, IEnumerable<CarDto>>(cars);

        return new Ok<IEnumerable<CarDto>>(result);
    }
}