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

        if (request.SearchParams.Radius <= 0)
            throw new ArgumentException($"{nameof(request.SearchParams.Radius)} must be >0");
        var carModel = await _ctx.CarModels.Include(x => x.Tariff)
            .Where(x => x.Tariff!.IsActive)
            .FirstOrDefaultAsync(x => x.Id == request.SearchParams.CarModelId, cancellationToken: cancellationToken);
        if (carModel == null) throw new ObjectNotFoundException(nameof(CarModel));


        var degreeDeviation = 0.001m * request.SearchParams.Radius / 111m;
        var cars = await _ctx.Cars
            .Where(x => x.CarModelId == request.SearchParams.CarModelId)
            .Where(x => !(x.HasToBeNonActive || x.IsTaken))
            .Where(x => (request.SearchParams.Latitude - degreeDeviation) <= x.ParkingLatitude
                        && x.ParkingLatitude <= (request.SearchParams.Latitude + degreeDeviation)
                        && (request.SearchParams.Longitude - degreeDeviation) <= x.ParkingLongitude
                        && x.ParkingLongitude <= (request.SearchParams.Longitude + degreeDeviation))
            .Take(request.Limit)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<List<Car>, IEnumerable<FreeCarDto>>(cars);
        // var result = cars.Select(x => new FreeCarDto
        // {
        //     CarId = x.Id, TariffId = carModel.TariffId, Location = new GeoPoint(x.ParkingLatitude, x.ParkingLongitude),
        //     Plate = x.LicensePlate
        // });
        
        return new Ok<IEnumerable<FreeCarDto>>(result);
    }
}