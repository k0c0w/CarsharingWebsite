using System.Text;
using System.Text.RegularExpressions;
using Contracts;
using Contracts.Results;
using Contracts.User;
using Contracts.UserInfo;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;

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

    public async Task<bool> Verify(int userInfoID)
    {
        try
        {
            var user = await FindUserInfo(userInfoID);
            user.Verified = true;
            _context.UserInfos.Update(user);
            _context.SaveChanges();
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
    
    
    public async Task<bool> EditUser(int id, EditUserDto? editUserVm)
    {
        try
        {
            var user = await FindUserInfo(id);
            CheckLastName(user,editUserVm.LastName);
            CheckFirstName(user,editUserVm.FirstName);
            CheckUserEmail(user,editUserVm.Email);
            CheckUserPhoneNum(user,editUserVm.PhoneNumber);
            CheckUserBirthday(user,editUserVm.BirthDay);
            CheckUserPassport(user,editUserVm.Passport);
            CheckUserPassportType(user,editUserVm.PassportType);
            CheckUserDriverLicense(user,editUserVm.DriverLicense);
            user.Verified = false;
            _context.UserInfos.Update(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    private async Task<UserInfo> FindUserInfo(int id)
    {
        var user = await _context.UserInfos
            .AsNoTracking()
            .Include(u => u.User)
            .Select(x => x)
            .Where(e => e.UserInfoId == id)
            .ToListAsync();
            
        return user.First();
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
    
    private void CheckLastName(UserInfo user,string val)
    {
        if(Regex.IsMatch(val, @"^[A-Z][a-zA-Z]*$"))
        {
            user.User.LastName = val;
        }
                 
    }
    private void CheckFirstName(UserInfo user, string val)
    {
        if(Regex.IsMatch(val, @"^[A-Z][a-zA-Z]*$"))
        {
            user.User.FirstName = val;
        }
            
    }
    private void CheckUserEmail(UserInfo user, string val)
    {
        if (Regex.IsMatch(val, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            user.User.Email = val;
            user.User.NormalizedEmail = val.ToUpper();
        }
    }
    private void CheckUserPhoneNum(UserInfo user, string val)
    {
        if (Regex.IsMatch(val, @"\d{10}$"))
        {
            user.User.PhoneNumber = val;
        }
    }
    private void CheckUserBirthday(UserInfo user, DateTime val)
    {
        if (DateTime.Now < val.Date)
        {
            user.BirthDay = val;
        }
    }
    private void CheckUserPassport(UserInfo user, string val)
    {
        if (Regex.IsMatch(val, @"\d{10}"))
        {
            user.Passport = val;
        }
    }
    private void CheckUserPassportType(UserInfo user, string val)
    {
        if (Regex.IsMatch(val, @"^[A-Z][a-zA-Z]*$"))
        {
            user.PassportType = val;
        }
    }
    private void CheckUserDriverLicense(UserInfo user, int val)
    {
        if (val.ToString().Length == 10)
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