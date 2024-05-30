using AutoMapper;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Features.CarManagement.Queries.GetAllModels;

public class GetAllModelsQueryHandler : IQueryHandler<GetAllModelsQuery, IEnumerable<CarModelDto>>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public GetAllModelsQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarModelDto>>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
    {
        var models = await _ctx.CarModels.ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<IEnumerable<CarModelDto>>(models);
        return new Ok<IEnumerable<CarModelDto>>(result);
    }
}