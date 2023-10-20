using Contracts;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;

namespace Services;

public class BookingService : IBookingService
{
    private readonly ICarService _carService;
    private readonly CarsharingContext _ctx;
    
    public BookingService(ICarService carService, CarsharingContext context)
    {
        _carService = carService;
        _ctx = context;
    }
    

    public async Task BookCarAsync(RentCarDto rentCarInfo)
    {
        if (rentCarInfo.Start > rentCarInfo.End || rentCarInfo.Days == 0) throw new ArgumentException("Wrong date bounds");
        var carSupportsTariff = await _ctx.Cars
            .Include(x => x.CarModel)
            .ThenInclude(x => x!.Tariff)
            .Where(x => x.Id == rentCarInfo.CarId)
            .FirstOrDefaultAsync();
        if (carSupportsTariff == null)
            throw new ObjectNotFoundException(
                $"car({rentCarInfo.CarId}) is not associated with tariff({rentCarInfo.TariffId})");
        var tariff = carSupportsTariff.CarModel!.Tariff!;
        if (rentCarInfo!.PotentialRenterUserId == null)
            throw new ObjectNotFoundException("");

        var userInfo = await GetConfirmedUserInfoAsync(rentCarInfo!.PotentialRenterUserId);
        var total = tariff!.Price * rentCarInfo.Days;
        if (userInfo.Balance < total) throw new InvalidOperationException("Not enough money to book car");
        await AssignCarToUserAsync(userInfo, rentCarInfo, tariff.Price);
    }

    private async Task AssignCarToUserAsync(UserInfo userInfo, RentCarDto details, decimal withdrawal)
    {
        var isAssigned = await _carService.SetCarIsTakenAsync(details.CarId);
        if(!isAssigned) throw new CarAlreadyBookedException();
        userInfo.Balance -= withdrawal;
        // сейчас аренда машины доступна только с текущего дня, без планирования
        var sub = new Subscription
        {
            StartDate = details.Start, 
            EndDate = details.End, 
            IsActive = true, 
            UserId = userInfo.UserId,
            CarId = details.CarId
        };
        _ctx.Subscriptions.Add(sub);
        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch
        {
            await _carService.ReleaseCarAsync(details.CarId);
        }
    }
    
    private async Task<UserInfo> GetConfirmedUserInfoAsync(string userId)
    {
        var user = await _ctx.Users.Include(x => x.UserInfo).FirstOrDefaultAsync( x=> x.Id== userId);
        var userInfo = user!.UserInfo;
        if (userInfo == null) throw new ObjectNotFoundException($"No such User with id:{userId}");
        if (!userInfo.Verified) throw new InvalidOperationException("Profile is not confirmed");
        return userInfo;
    }
}