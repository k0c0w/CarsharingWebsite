using Domain;
using Domain.Entities;
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
    public async Task<List<UserInfo>> GettAllInfoAsync()
    {
        var userInfos = await _context.UserInfos.ToListAsync();
        if (!(userInfos.Count == 0))
        {
            throw new ObjectNotFoundException("Can't find any user's information");
        }
        else
        {
            return userInfos;
        }
        
    }
}