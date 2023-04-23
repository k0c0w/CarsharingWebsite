using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class CarService
{
    private readonly CarsharingContext _ctx;
    
    public CarService(CarsharingContext context)
    {
        _ctx = context;
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