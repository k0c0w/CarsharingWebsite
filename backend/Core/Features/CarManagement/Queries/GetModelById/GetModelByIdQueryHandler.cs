using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Queries.GetModelById;

public class GetModelByIdQueryHandler : IQueryHandler<GetModelByIdQuery, ExtendedCarModelDto>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public GetModelByIdQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<ExtendedCarModelDto>> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _ctx.CarModels.Where(x => x.Id == request.Id)
            .Include(x => x.Tariff)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (model == null) 
            return new Error<ExtendedCarModelDto>($"{new ObjectNotFoundException(nameof(CarModel)).Message}");

        // return new ExtendedCarModelDto
        // {
        //     Brand = model.Brand,
        //     Description = model.Description,
        //     Id = model.Id,
        //     Model = model.Model,
        //     ImageUrl = CarModelDto.GenerateImageUrl(model.ImageName),
        //     TariffId = model.TariffId,
        //     Price = model!.Tariff!.Price,
        //     Restrictions = model.Tariff.MaxMileage,
        //     TariffName = model.Tariff.Name
        // };
        
        var result = _mapper.Map<ExtendedCarModelDto>(model);
        return new Ok<ExtendedCarModelDto>(result);
    }
}