using System.Text.RegularExpressions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Migrations.CarsharingApp;

namespace Features.Users.Shared;

public partial class UserValidation
{
    [GeneratedRegex(@"^[^$&+,:;=?@#|<>. -^*)(%!\""/№_}\[\]{{~]*$")]
    private static partial Regex GetNameRegexGenerated();

    [GeneratedRegex(@"\d{6}")]
    private static partial Regex GetPassportRegexGenerated();

    [GeneratedRegex(@"\d{4}")]
    private static partial Regex GetPassportTypeRegexGenerated();
    
    private readonly UserManager<User> _userManager;
    private readonly CarsharingContext _context;

    public UserValidation(UserManager<User> userManager, CarsharingContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    internal static void CheckName(Domain.Entities.User user, string val)
    {
        if(!string.IsNullOrEmpty(val) && GetNameRegexGenerated().IsMatch(val))
        {
            user.FirstName = val;
        }
    }

    internal static void CheckLastName(Domain.Entities.User user, string val)
    {
        if(!string.IsNullOrEmpty(val) && GetNameRegexGenerated().IsMatch(val))
        {
            user.LastName = val;
        }
                 
    }

    internal async Task<bool> CheckUserEmail(Domain.Entities.User user, string val)
    {
        var existeduser = await  _userManager.FindByEmailAsync(val);
        if (existeduser != null && existeduser.Id != user.Id) return false;
        user.Email = val;
        user.NormalizedEmail = val.ToUpper();
        user.EmailConfirmed = false;
        return true;
    }

    internal static void CheckUserBirthday(UserInfo user, DateTime val)
    {
        if (DateTime.Now > val.Date)
        {
            user.BirthDay = val;
        }
    }

    internal static void CheckUserPassport(UserInfo user, string val)
    {
        if (!string.IsNullOrEmpty(val) && GetPassportRegexGenerated().IsMatch(val))
        {
            user.Passport = val;
        }
    }

    internal static void CheckUserPassportType(UserInfo user, string val)
    {
        if (!string.IsNullOrEmpty(val) && GetPassportRegexGenerated().IsMatch(val))
        {
            user.PassportType = val;
        }
    }
}