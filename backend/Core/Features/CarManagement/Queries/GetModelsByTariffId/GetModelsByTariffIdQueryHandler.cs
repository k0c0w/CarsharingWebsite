using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

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
        // var result = models.Select(x => new CarModelDto
        // {
        //     Brand = x.Brand,
        //     Description = x.Description,
        //     Model = x.Model,
        //     Id = x.Id,
        //     TariffId = x.TariffId,
        //     ImageUrl = CarModelDto.GenerateImageUrl(x.ImageName)
        // });
        return new Ok<IEnumerable<CarModelDto>>(result);
    }
}