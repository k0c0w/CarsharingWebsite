using Contracts;
using Domain.Entities;
using Features.CarManagement;
using Features.CarManagement.Commands.ReleaseCar;
using Features.CarManagement.Commands.SetCarTaken;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Services.Abstractions;
using Services.Exceptions;

namespace Services;

public class BookingService : IBookingService
{
    private readonly CarsharingContext _ctx;
    private readonly IMediator _mediator; 
    
    public BookingService(CarsharingContext context, IMediator mediator)
    {
        _ctx = context;
        _mediator = mediator;
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
        //TODO: Проверить cqrs 
        var isAssigned = await _mediator.Send(new SetCarTakenCommand(details.CarId)); 
        
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
            //TODO: Проверить корректность 
            await _mediator.Send(new ReleaseCarCommand(details.CarId));
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