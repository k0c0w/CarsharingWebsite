using Contracts;
using Contracts.Results;
using Contracts.User;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;

namespace Services.User;

public class UserService : IUserInfoService
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly CarsharingContext _context;

    public UserService(UserManager<Domain.Entities.User> manager, CarsharingContext context)
    {
        _context = context;
        _userManager = manager;
    }
    
    public async Task<UserInfoDto> GetPersonalInfoAsync(string userId)
    {
        var user = await GetUserInfoAsync(userId);
        return Map(user);
    }

    public async Task<ProfileInfoDto> GetProfileInfoAsync(string userId)
    {
        var user = await GetUserInfoAsync(userId);
        var userSubscriptions = await _context.Subscriptions
            .Where(x => x.IsActive)
            .Where(x => x.UserId == userId)
            .Include( x=> x.Car)
                .ThenInclude(x=> x.CarModel)
            .ToListAsync();
        var bookedCars = userSubscriptions
            .Where(x => !x.IsExpired)   
            .OrderByDescending(x => x.EndDate)
            .Select(x => x.Car)
            .Select(x => new CarShortcutDto
            {
                Brand = x.CarModel.Brand,
                Model = x.CarModel.Model,
                Id = x.Id,
                IsOpened = x.IsOpened,
                LicensePlate = x.LicensePlate
            })
            .ToArray();

        return new ProfileInfoDto
        {
            PersonalInfo = Map(user),
            CurrentlyBookedCars = bookedCars
        };
    }

    public async Task<PasswordChangeResult> ChangePassword(string userId, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new ObjectNotFoundException(nameof(User));
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return new PasswordChangeResult(result.Succeeded, result.Errors.Select(x => x.Description));
    }

    private async Task<Domain.Entities.User> GetUserInfoAsync(string userId)
    {
        var user = await _context.Users.Include(x => x.UserInfo).FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new ObjectNotFoundException(nameof(User));
        if (user.UserInfo == null)
        {
            //todo: call logger here since active user must have userinfo
            throw new InvalidOperationException();
        }

        return user;
    }

    private UserInfoDto Map(Domain.Entities.User user)
    {
        var info = user.UserInfo;
        return new UserInfoDto
        {
            UserId = user.Id,
            Balance = info.Balance,
            Passport = $"{info.PassportType}{info.Passport}",
            BirthDate = info.BirthDay,
            DriverLicense = info.DriverLicense,
            FirstName = user.FirstName,
            LastName = user.Surname,
            Email = user.Email
        };
    }
}