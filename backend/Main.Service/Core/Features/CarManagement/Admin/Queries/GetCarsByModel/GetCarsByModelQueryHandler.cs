using AutoMapper;
using Contracts;
using Domain.Entities;
using Entities.Repository;

namespace Features.CarManagement.Admin.Queries.GetCarsByModel;

public class GetCarsByModelQueryHandler : IQueryHandler<GetCarsByModelQuery, IEnumerable<CarDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarsByModelQueryHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarDto>>> Handle(GetCarsByModelQuery request, CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetCarsByModelIdAsync(request.ModelId);

        var result = _mapper.Map<IEnumerable<Car>, IEnumerable<CarDto>>(cars);
        return new Ok<IEnumerable<CarDto>>(result);
    }
}