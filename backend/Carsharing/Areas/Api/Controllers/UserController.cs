using System.Text.RegularExpressions;
using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Area("Api")]
public class UserController : Controller
{
    private readonly CarsharingContext _carsharingContext;

    public UserController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegistrationDto dto)
    {
        var isEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(dto.Email);
        var isName = new Regex(@"^[A-Z][a-zA-Z]*$").IsMatch(dto.Name);
        var isSurname = new Regex(@"^[A-Z][a-zA-Z]*$").IsMatch(dto.Surname);
        var isPassword = true;//new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").IsMatch(dto.Password);
        
        if (!isEmail)
            return BadRequest("Wrong email format");
        
        if (!isPassword)
            return BadRequest("Wrong password format");
        
        if (!isSurname)
            return BadRequest("Wrong surname format");
        
        if (!isName) 
            return BadRequest("Wrong name format");
        
        if (dto.Password != dto.RetryPassword)
        {
            return BadRequest("Wrong confirm password");
        }

        var client = await _carsharingContext.Users.FirstOrDefaultAsync(cl => cl.Email == dto.Email);
        if (client != null)
        {
            return BadRequest("A client with this email already exists");
        }

        var id = await _carsharingContext.Users.CountAsync() + 1;
        var newClient =
            new User
            {
                Email = dto.Email,
                UserInfo = new UserInfo
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    BirthDay = dto.Birthday,
                    UserId = id,
                    PassportType = "passport"
                },
                PasswordHash = dto.Password,
                RoleId = Roles.Admin
            };
        await _carsharingContext.AddAsync(newClient);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginDto dto)
    {
        var client = await _carsharingContext.Users.FirstOrDefaultAsync(cl => cl.Email == dto.Email);

        if (client == null)
        {
            return Unauthorized("There is no client with this email");
        }

        if (client.PasswordHash != dto.Password)
        {
            return Unauthorized("Wrong password");
        }
        
        return Ok();
    }
}