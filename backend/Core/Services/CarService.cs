using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;

namespace Services;

public class CarService : ICarService
{
    private readonly CarsharingContext _ctx;
    
    public CarService(CarsharingContext context)
    {
        _ctx = context;
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

    public async Task CreateCarModel()
    {
        
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
}