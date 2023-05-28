using System.Text.RegularExpressions;
using Contracts;
using Contracts.Results;
using Contracts.User;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;
using EditUserDto = Contracts.UserInfo.EditUserDto;

namespace Services.User;

public class UserService : IUserService
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly CarsharingContext _context;

    public UserService(UserManager<Domain.Entities.User> manager, CarsharingContext context)
    {
        _context = context;
        _userManager = manager;
    }

    public async Task<UserInfo?> GetUserInfoByIdAsync(string id)
    {
        return await _context.UserInfos
            .AsNoTracking()
            .Include(u => u.User)
            .FirstOrDefaultAsync(x => x.UserId == id);
    }

    public async Task<UserInfoDto> GetPersonalInfoAsync(string userId)
    {
        var user = await GetUserWithInfoAsync(userId);
        return Map(user);
    }

    public async Task<ProfileInfoDto> GetProfileInfoAsync(string userId)
    {
        var user = await GetUserWithInfoAsync(userId);
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

    public async Task<bool> Verify(string userId)
    {
        try
        {
            var user = (await GetUserWithInfoAsync(userId)).UserInfo;
            user.Verified = true;
            //todo: убрать когда будет подтверждение по почте
            user.User.EmailConfirmed = true;
            _context.UserInfos.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public async Task<List<UserInfo>> GetAllInfoAsync()
    {

        var userInfos = await _context.UserInfos
                .AsNoTracking()
                .Include(u => u.User)
                .ToListAsync();
        return userInfos;
    }
    
    
    public async Task<bool> EditUser(string userId, EditUserDto? editUserDto)
    {
        try
        {
            var user = await GetUserWithInfoAsync(userId);
            if(! await CheckUserEmail(user,editUserDto.Email)) {throw new Exception("Почта уже зарегестрирова");}
            CheckLastName(user,editUserDto.LastName);
            CheckName(user,editUserDto.FirstName);
            CheckUserBirthday(user.UserInfo,editUserDto.BirthDay);
            CheckUserPassport(user.UserInfo,editUserDto.Passport);
            CheckUserPassportType(user.UserInfo,editUserDto.PassportType);
            user.UserInfo.Verified = false;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private async Task<Domain.Entities.User> GetUserWithInfoAsync(string userId)
    {
        var user = await _context.Users
            .Include(x => x.UserInfo).FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new ObjectNotFoundException(nameof(User));
        if (user.UserInfo == null)
        {
            //todo: call logger here since active user must have userinfo
            throw new InvalidOperationException();
        }

        return user;
    }
    
    private void CheckName(Domain.Entities.User user,string val)
    {
        if(!string.IsNullOrEmpty(val) && Regex.IsMatch(val, @"^[^$&+,:;=?@#|<>. -^*)(%!\""/№_}\[\]{{~]*$"))
        {
            user.FirstName = val;
        }
                 
    }
    private void CheckLastName(Domain.Entities.User user,string val)
    {
        if(!string.IsNullOrEmpty(val) && Regex.IsMatch(val, @"^[^$&+,:;=?@#|<>. -^*)(%!\""/№_}\[\]{{~]*$"))
        {
            user.LastName = val;
        }
                 
    }
    private async Task<bool> CheckUserEmail(Domain.Entities.User user, string val)
    {
        var existeduser = await  _userManager.FindByEmailAsync(val);
        if (existeduser != null && existeduser.Id != user.Id) return false;
        user.Email = val;
        user.NormalizedEmail = val.ToUpper();
        user.EmailConfirmed = false;
        return true;
    }
    private async Task<bool> CheckUserPhoneNum(Domain.Entities.User user, string val)
    {
        if (Regex.IsMatch(val, @"\d{10}$"))
        {
            var users = await GetAllInfoAsync();
            users = users.Select(x => x).Where(x => x.User.Id != user.Id).ToList();
            foreach (var el in users)
            {
                if (el.User.PhoneNumber == val)
                {
                    return false;
                }
            }
            user.PhoneNumber = val;
            user.PhoneNumberConfirmed = false;
            return true;
        }
        return false;
    }
    private void CheckUserBirthday(UserInfo user, DateTime val)
    {
        if (DateTime.Now > val.Date)
        {
            user.BirthDay = val;
        }
    }
    private void CheckUserPassport(UserInfo user, string val)
    {
        if (!string.IsNullOrEmpty(val) && Regex.IsMatch(val, @"\d{6}"))
        {
            user.Passport = val;
        }
    }
    private void CheckUserPassportType(UserInfo user, string val)
    {
        if (!string.IsNullOrEmpty(val) && Regex.IsMatch(val, @"\d{4}"))
        {
            user.PassportType = val;
        }
    }
    private void CheckUserDriverLicense(UserInfo user, int? val)
    {
        if (val is > 0 and <= 999999999 )
        {
            user.DriverLicense = val;
        }
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
            LastName = user.LastName,
            Email = user.Email
        };
    }
}