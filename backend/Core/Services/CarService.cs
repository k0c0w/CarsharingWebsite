using Contracts;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using Services.Abstractions.Admin;
using Services.Exceptions;
using File = Contracts.File;

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
            //Поч два раза isOpened?
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

    public async Task<bool> SetCarHasToBeNonActiveAsync(int id)
    {
        try
        {
            var requestedCar = await _ctx.Cars.FindAsync(id);
            if (requestedCar.IsTaken || requestedCar.HasToBeNonActive) return false;
            requestedCar.HasToBeNonActive = true;
            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            //логировать что машина уже не активна?
        }

        return false;
    }

    public async Task<IEnumerable<CarModelDto>> GetModelsByTariffIdAsync(int tariff)
    {
        var models = await _ctx.CarModels.Where(x => x.TariffId == tariff).ToListAsync();
        return models.Select(x => new CarModelDto
        {
            Brand = x.Brand,
            Description = x.Description,
            Model = x.Model,
            Id = x.Id,
            TariffId = x.TariffId,
            ImageUrl = CarModelDto.GenerateImageUrl(x.ImageName)
        });
    }

    public async Task<ExtendedCarModelDto> GetModelByIdAsync(int id)
    {
        var model = await _ctx.CarModels.Where(x => x.Id == id)
            .Include(x => x.Tariff)
            .FirstOrDefaultAsync();
        if (model == null) throw new ObjectNotFoundException(nameof(CarModel));
        return new ExtendedCarModelDto
        {
            Brand = model.Brand,
            Description = model.Description,
            Id = model.Id,
            Model = model.Model,
            ImageUrl = CarModelDto.GenerateImageUrl(model.ImageName),
            TariffId = model.TariffId,
            Price = model.Tariff.Price,
            Restrictions = model.Tariff.MaxMileage,
            TariffName = model.Tariff.Name
        };
    }

    public async Task<IEnumerable<FreeCarDto>> GetAvailableCarsByLocationAsync(SearchCarDto searchParams, int limit=256)
    {
        if (searchParams.Radius <= 0) 
            throw new ArgumentException($"{nameof(searchParams.Radius)} must be >0");
        var carModel = await _ctx.CarModels.Include(x => x.Tariff)
            .Where(x => x.Tariff.IsActive)
            .FirstOrDefaultAsync(x => x.Id == searchParams.CarModelId);
        if (carModel == null) throw new ObjectNotFoundException(nameof(CarModel));
        
        
        var degreeDeviation = 0.001m * searchParams.Radius / 111m;
        var cars = await _ctx.Cars
            .Where(x => x.CarModelId == searchParams.CarModelId)
            .Where(x => !(x.HasToBeNonActive || x.IsTaken))
            .Where(x => (searchParams.Latitude - degreeDeviation) <= x.ParkingLatitude
                        && x.ParkingLatitude <= (searchParams.Latitude + degreeDeviation)
                        && (searchParams.Longitude - degreeDeviation) <= x.ParkingLongitude
                        && x.ParkingLongitude <= (searchParams.Longitude + degreeDeviation))
            .Take(limit)
            .ToListAsync();
        return cars.Select(x => new FreeCarDto
            { CarId = x.Id, TariffId = carModel.TariffId, Location = new GeoPoint(x.ParkingLatitude, x.ParkingLongitude) });
    }

    public async Task<IEnumerable<CarDto>> GetAvailableCarsByModelAsync(int modelId)
    {
        var cars = await CarsByModelId(modelId)
            .Where(x => !x.IsTaken && !x.HasToBeNonActive)
            .ToListAsync();
        return cars.Select(x => new CarDto
        {
            Id = x.Id,
            IsOpened = x.IsOpened,
            IsTaken = x.IsTaken,
            LicensePlate = x.LicensePlate,
            ParkingLatitude = x.ParkingLatitude,
            ParkingLongitude = x.ParkingLongitude,
            CarModelId = x.CarModelId,
            HasToBeNonActive = x.HasToBeNonActive
        });
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

        var photo = create.ModelPhoto with { Name = model.ImageName };
        await _fileProvider.SaveAsync(Path.Combine("wwwroot", "models"), photo);
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
        var old = await _ctx.CarModels.FindAsync(id);
        if (old == null) throw new ObjectNotFoundException(nameof(CarModel));
        
        if (update.Description != null)
            old.Description = update.Description;
        if (update.Image != null)
            await UpdateModelImage(old, update.Image);
        
        _ctx.CarModels.Update(old);
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

    public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
    {
        var cars = await _ctx.Cars.ToListAsync();
        return cars.Select(x => new CarDto
        {
            Id = x.Id,
            IsOpened = x.IsOpened,
            IsTaken = x.IsTaken,
            LicensePlate = x.LicensePlate,
            ParkingLatitude = x.ParkingLatitude,
            ParkingLongitude = x.ParkingLongitude,
            CarModelId = x.CarModelId,
            HasToBeNonActive = x.HasToBeNonActive
        });
    }

    public async Task<IEnumerable<CarDto>> GetCarsByModelAsync(int modelId)
    {
        var cars = await CarsByModelId(modelId).ToListAsync();
        return cars.Select(x => new CarDto()
        {
            Id = x.Id,
            IsOpened = x.IsOpened,
            IsTaken = x.IsTaken,
            LicensePlate = x.LicensePlate,
            ParkingLatitude = x.ParkingLatitude,
            ParkingLongitude = x.ParkingLongitude,
            CarModelId = x.CarModelId,
            HasToBeNonActive = x.HasToBeNonActive
        });
    }

    private async Task UpdateModelImage(CarModel old, IFile file)
    {
        var folder = Path.Combine("wwwroot", "models");
        try
        {
            _fileProvider.Delete(folder, old.ImageName);
        }
        catch(DirectoryNotFoundException) {}

        await _fileProvider.SaveAsync(folder, new  File {Content = file.Content, Name = old.ImageName});
    }

    private IQueryable<Car> CarsByModelId(int id) => _ctx.Cars.Where(x => x.CarModelId == id);
}