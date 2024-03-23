using Domain.Entities;
using Domain.Repository;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Persistence.UnitOfWork;

namespace Persistence.RepositoryImplementation;

public class CarRepository : ICarRepository
{
    private readonly CarsharingContext _ctx;

    public CarRepository(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task AddAsync(Car entity)
    {
        await _ctx.Cars.AddAsync(entity);
    }

    public Task<bool> ExistsByIdAsync(int carId)
    {
        return _ctx.Cars
            .AnyAsync(x => x.Id == carId);
    }

    public async Task<IEnumerable<Car>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        IQueryable<Car> cars = _ctx.Cars;

        if (offset != null)
            cars = cars.Skip(offset!.Value);

        if (limit != null)
            cars = cars.Take(limit!.Value);

        return await cars
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task<Car?> GetByIdAsync(int primaryKey)
    {
        return await _ctx.Cars
            .SingleOrDefaultAsync(x => x.Id == primaryKey);
    }

    public async Task<Tariff?> GetRelatedTariffAsync(int carId)
    {
        var car = await _ctx.Cars
            .Include(x => x.CarModel)
            .ThenInclude(x => x!.Tariff)
            .FirstOrDefaultAsync(x => x.Id == carId);

        if (car == null)
            return null;

        return car!.CarModel!.Tariff;
    }

    public async Task RemoveByIdAsync(int primaryKey)
    {
        var car = await GetByIdAsync(primaryKey) ?? throw new NotFoundException("Car does not exist.", typeof(Car));

        await RemoveAsync(car);
    }

    public Task RemoveAsync(Car car)
    {
        _ctx.Cars.Remove(car);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Car entity)
    {
        _ctx.Cars.Update(entity);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Car>> GetCarsByModelIdAsync(int modelId)
    {
        return await _ctx.Cars
            .Where(x => x.CarModelId == modelId)
            .AsNoTracking()
            .ToArrayAsync();
    }

    public Task<Car?> GetByLiciensePlateAsync(string lp)
    {
        return _ctx.Cars
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.LicensePlate == lp);
    }
}
