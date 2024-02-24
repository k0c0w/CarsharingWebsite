using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Admin.Queries.GetCarsByModel;

public class GetCarsByModelQueryHandler : IQueryHandler<GetCarsByModelQuery, IEnumerable<CarDto>>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public GetCarsByModelQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarDto>>> Handle(GetCarsByModelQuery request, CancellationToken cancellationToken)
    {
        var cars = await CarsByModelId(request.ModelId).ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<List<Car>, IEnumerable<CarDto>>(cars);
        return new Ok<IEnumerable<CarDto>>(result);
    }

    private IQueryable<Car> CarsByModelId(int id) => _ctx.Cars.Where(x => x.CarModelId == id);
}