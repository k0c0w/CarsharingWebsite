using Contracts;
using Entities;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;

namespace Services;

public class RentingService
{
    private CarService _carService;
    private CarsharingContext _ctx;
    
    public RentingService(CarService carService, CarsharingContext context)
    {
        _carService = carService;
        _ctx = context;
    }

    public async Task BookCar(RentCarDto rentCarInfo)
    {
        //todo: както локать баланс
        var userInfo = await GetConfirmedUserInfo(rentCarInfo.PotentialRenterUserId);
        var tariff = await _ctx.Tariffs.FindAsync(rentCarInfo.TariffId);
        var total = tariff.Price * rentCarInfo.Days;
        if (userInfo.Balance < total) throw new InvalidOperationException("Not enough money to book car");
        await AssignCarToUser(userInfo, rentCarInfo, tariff.Price);
    }

    private async Task AssignCarToUser(UserInfo userInfo, RentCarDto details, decimal withdrawal)
    {
        var isAssigned = await _carService.SetCarIsTakenAsync(details.CarId);
        if(!isAssigned) return;
        //todo: вынести снятие со счета
        userInfo.Balance -= withdrawal;
        //todo: сейчас аренда машины доступна только с текущего дня, без планирования
        var sub = new Subscription()
        {
            StartDate = details.Start, 
            EndDate = details.End, 
            IsActive = true, 
            UserId = details.PotentialRenterUserId,
            CarId = details.CarId
        };
        _ctx.Subscriptions.Add(sub);
        await _ctx.SaveChangesAsync();
        //todo: release car on error carservice.ReleaseCar(carid);
    }
    
    private async Task<UserInfo> GetConfirmedUserInfo(int userId)
    {
        var userInfo = await _ctx.UserInfos.FindAsync(userId);
        if (userInfo == null) throw new ObjectNotFoundException($"No such UserInfo with id:{userId}");
        //todo: isConfirmed переделать
        var isConfirmed = userInfo.PassportType != null && userInfo.Passport != null 
                                                        && userInfo.DriverLicense != null && userInfo.TelephoneNum != null;
        if (isConfirmed) throw new InvalidOperationException("Profile is not confirmed");
        return userInfo;
    }
}