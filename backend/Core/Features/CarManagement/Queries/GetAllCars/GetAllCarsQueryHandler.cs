using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Queries.GetAllCars;

public class GetAllCarsQueryHandler : IQueryHandler<GetAllCarsQuery, IEnumerable<CarDto>>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public GetAllCarsQueryHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CarDto>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _ctx.Cars.ToListAsync(cancellationToken: cancellationToken);
        // var result = cars.Select(x => new CarDto
        // {
        //     Id = x.Id,
        //     IsOpened = x.IsOpened,
        //     IsTaken = x.IsTaken,
        //     LicensePlate = x.LicensePlate,
        //     ParkingLatitude = x.ParkingLatitude,
        //     ParkingLongitude = x.ParkingLongitude,
        //     CarModelId = x.CarModelId,
        //     HasToBeNonActive = x.HasToBeNonActive
        // });

        var result = _mapper.Map<List<Car>, IEnumerable<CarDto>>(cars);

        return new Ok<IEnumerable<CarDto>>(result);
    }
}