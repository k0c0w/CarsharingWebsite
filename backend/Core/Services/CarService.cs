using Contracts;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Abstractions.Admin;

namespace Services;

public class CarService : IAdminCarService
{
    private readonly CarsharingContext _ctx;
    private readonly IFileProvider _fileProvider;
    
    public CarService(CarsharingContext context, IFileProvider fileProvider)
    {
        _ctx = context;
        _fileProvider = fileProvider;
    }

    public async Task ReleaseCarAsync(int carId)
    {
        var car = await _ctx.Cars.FindAsync(carId);
        if (car != null)
        {
            car.IsOpened = false;
            car.IsOpened = false;
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<bool> SetCarIsTakenAsync(int id)
    {
        try
        {
            var requestedCar = await _ctx.Cars.FindAsync(id);
            //if (requestedCar.HasToBeNonActive) throw new InvalidOperationException("Booked by administrator");
            if (requestedCar.IsTaken || requestedCar.HasToBeNonActive) return false;
            requestedCar.IsTaken = true;
            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            //логировать что машина уже занята?
        }

        return false;
    }

    public async Task CreateModelAsync(CreateCarModelDto create)
    {
        var model = new CarModel
        {
            Brand = create.Brand,
            Description = create.Description,
            Model = create.Model,
            TariffId = create.TariffId,
        };

        await _ctx.CarModels.AddAsync(model);
        await _ctx.SaveChangesAsync();

        await _fileProvider.SaveAsync(Path.Combine("wwwroot", "models"), create.ModelPhoto);
    }

    public async Task CreateCarAsync(CreateCarDto create)
    {
        var car = new Car
        {
            CarModelId = create.CarModelId,
            ParkingLatitude = create.ParkingLatitude,
            ParkingLongitude = create.ParkingLongitude,
            LicensePlate = create.LicensePlate,
        };
        await _ctx.Cars.AddAsync(car);
        await _ctx.SaveChangesAsync();
    }

    public async Task EditModelAsync(int id, EditCarModelDto update)
    {
        var model = new CarModel
        {
            Id = id,
            Brand = update.Brand,
            Description = update.Description,
            Model = update.Model
        };
        _ctx.CarModels.Update(model);
        await _ctx.SaveChangesAsync();
    }

    public async Task TryDeleteModelAsync(int id)
    {
        if (await _ctx.Cars.AnyAsync(x => x.CarModelId == id))
            throw new InvalidOperationException();
        var model = await _ctx.CarModels.FirstAsync(x => x.Id == id);
        _ctx.CarModels.Remove(model);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteCarAsync(int id)
    {
        var car = await _ctx.Cars.FirstAsync(x => x.Id == id);
        var bindSubs = await _ctx.Subscriptions.Where(x => x.CarId == id).ToListAsync();
        _ctx.Subscriptions.UpdateRange(bindSubs.Select(x =>
        {
            x.CarId = null;
            return x;
        }));
        _ctx.Cars.Remove(car);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<CarModelDto>> GetAllModelsAsync()
    {
        var models = await _ctx.CarModels.ToListAsync();
        return models.Select(x => new CarModelDto
        {
            Id = x.Id,
            Brand = x.Brand,
            Model = x.Model,
            Description = x.Description,
            TariffId = x.TariffId
        });
    }
}