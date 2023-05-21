using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Contracts.UserInfo;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Exceptions;

namespace Services;

public class UserInfoService : IUserInfoService
{
    private readonly CarsharingContext _context;
    public UserInfoService(CarsharingContext context)
    {
        _context = context;
    }

    public async Task<bool> Verify(int userInfoID)
    {
        try
        {
            var user = await FindUserInfo(userInfoID);
            user.Verifyed = true;
            _context.UserInfos.Update(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public async Task<List<UserInfo>> GettAllInfoAsync()
    {
        var userInfos = await _context.UserInfos
                                    .AsNoTracking()
                                    .Include(u => u.User)
                                    .ToListAsync();
        return userInfos;
    }

    public async Task<UserInfo> FindUserInfo(int id)
    {
        var user = await _context.UserInfos
            .AsNoTracking()
            .Include(u => u.User)
            .Select(x => x)
            .Where(e => e.UserInfoId == id)
            .ToListAsync();
            
        return user.First();
    }
    
    public async Task<bool> EditUser(int id, EditUserDto? editUserVm)
    {
        try
        {
            var user = await FindUserInfo(id);
            CheckUserSurname(user,editUserVm.UserSurname);
            CheckUserName(user,editUserVm.UserName);
            CheckUserEmail(user,editUserVm.Email);
            CheckUserPhoneNum(user,editUserVm.PhoneNumber);
            CheckUserBirthday(user,editUserVm.BirthDay);
            CheckUserPassport(user,editUserVm.Passport);
            CheckUserPassportType(user,editUserVm.PassportType);
            CheckUserDriverLicense(user,editUserVm.DriverLicense);
            user.Verifyed = false;
            _context.UserInfos.Update(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    private void CheckUserSurname(UserInfo user,string val)
    {
        if (Regex.IsMatch(val, @"^[A-Z][a-zA-Z]*$"))
        {
                user.User.UserSurname = val;  
        }
    }
    private void CheckUserName(UserInfo user, string val)
    {
        if (Regex.IsMatch(val, @"^[A-Z][a-zA-Z]*$"))
        {
            user.User.UserName = val;
            user.User.NormalizedUserName = val.ToUpper();
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
        if (Regex.IsMatch(val, @"\d{10}"))
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
}