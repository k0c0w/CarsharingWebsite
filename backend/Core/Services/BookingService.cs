using Contracts;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;

namespace Services;

public class BookingService : IBookingService
{
    private ICarService _carService;
    private CarsharingContext _ctx;
    
    public BookingService(ICarService carService, CarsharingContext context)
    {
        _carService = carService;
        _ctx = context;
    }
    

    public async Task BookCarAsync(RentCarDto rentCarInfo)
    {
        if (rentCarInfo.Start > rentCarInfo.End || rentCarInfo.Days == 0) throw new ArgumentException("Wrong date bounds");
        var tariff = await _ctx.Tariffs.FindAsync(rentCarInfo.TariffId);
        if (tariff == null) throw new ObjectNotFoundException($"No such tariff: id {rentCarInfo.TariffId}");
        var carSupportsTariff = await _ctx.Cars.Include(x => x.CarModel)
            .Where(x => x.Id == rentCarInfo.CarId && x.CarModel.TariffId == rentCarInfo.TariffId)
            .AnyAsync();
        if (!carSupportsTariff)
            throw new ObjectNotFoundException(
                $"car({rentCarInfo.CarId}) is not associated with tariff({rentCarInfo.TariffId})");
        
        var userInfo = await GetConfirmedUserInfoAsync(rentCarInfo.PotentialRenterUserInfoId);
        var total = tariff.Price * rentCarInfo.Days;
        if (userInfo.Balance < total) throw new InvalidOperationException("Not enough money to book car");
        await AssignCarToUserAsync(userInfo, rentCarInfo, tariff.Price);
    }

    private async Task AssignCarToUserAsync(UserInfo userInfo, RentCarDto details, decimal withdrawal)
    {
        var isAssigned = await _carService.SetCarIsTakenAsync(details.CarId);
        if(!isAssigned) throw new CarAlreadyBookedException();
        userInfo.Balance -= withdrawal;
        //todo: сейчас аренда машины доступна только с текущего дня, без планирования
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
    
    private async Task<UserInfo> GetConfirmedUserInfoAsync(int userInfoId)
    {
        var userInfo = await _ctx.UserInfos.FindAsync(userInfoId);
        if (userInfo == null) throw new ObjectNotFoundException($"No such UserInfo with id:{userInfoId}");
        if (!userInfo.Verified) throw new InvalidOperationException("Profile is not confirmed");
        return userInfo;
    }
}