using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Features.CarManagement.Queries.GetModelsByTariffId;

public class GetModelsByTariffIdQueryHandler : IQueryHandler<GetModelsByTariffIdQuery, IEnumerable<CarModelDto>>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public GetModelsByTariffIdQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarModelDto>>> Handle(GetModelsByTariffIdQuery request, CancellationToken cancellationToken)
    {
        var models = await _ctx.CarModels.Where(x => x.TariffId == request.TariffId).ToListAsync();
        var result = _mapper.Map<List<CarModel>, IEnumerable<CarModelDto>>(models);
        return new Ok<IEnumerable<CarModelDto>>(result);
    }
}