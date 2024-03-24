using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Queries.GetAvailableCarsByLocation;

public class GetAvailableCarsByLocationQueryHandler : IQueryHandler<GetAvailableCarsByLocationQuery, IEnumerable<FreeCarDto>>
{
    private readonly CarsharingContext _ctx;
    public required IMapper _mapper;

    public GetAvailableCarsByLocationQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<FreeCarDto>>> Handle(GetAvailableCarsByLocationQuery request,
        CancellationToken cancellationToken)
    {

        if (request.Radius <= 0)
            throw new ArgumentException($"{nameof(request.Radius)} must be >0");
        var carModel = await _ctx.CarModels.Include(x => x.Tariff)
            .Where(x => x.Tariff!.IsActive)
            .FirstOrDefaultAsync(x => x.Id == request.CarModelId, cancellationToken: cancellationToken) ?? throw new ObjectNotFoundException(nameof(CarModel));

        var degreeDeviation = 0.001m * request.Radius / 111m;
        var cars = await _ctx.Cars
            .Where(x => x.CarModelId == request.CarModelId)
            .Where(x => !(x.HasToBeNonActive || x.IsTaken || x.Prebooked))
            .Where(x => (request.Latitude - degreeDeviation) <= x.ParkingLatitude
                        && x.ParkingLatitude <= (request.Latitude + degreeDeviation)
                        && (request.Longitude - degreeDeviation) <= x.ParkingLongitude
                        && x.ParkingLongitude <= (request.Longitude + degreeDeviation))
            .Take(request.Limit)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<List<Car>, IEnumerable<FreeCarDto>>(cars);
        
        return new Ok<IEnumerable<FreeCarDto>>(result);
    }
}