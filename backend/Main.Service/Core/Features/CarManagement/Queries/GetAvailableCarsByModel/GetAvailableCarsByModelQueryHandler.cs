using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Features.CarManagement.Queries.GetAvailableCarsByModel;

public class GetAvailableCarsByModelQueryHandler : IQueryHandler<GetAvailableCarsByModelQuery, IEnumerable<CarDto>>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public GetAvailableCarsByModelQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarDto>>> Handle(GetAvailableCarsByModelQuery request, CancellationToken cancellationToken)
    {
        var cars = await _ctx.Cars.Where(x => x.CarModelId == request.ModelId)
            .Where(x => !(x.IsTaken || x.HasToBeNonActive || x.Prebooked))
            .ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<List<Car>, IEnumerable<CarDto>>(cars);
        
        return new Ok<IEnumerable<CarDto>>(result);
    }
}