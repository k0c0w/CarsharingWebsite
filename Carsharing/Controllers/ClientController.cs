using System.Text.RegularExpressions;
using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Carsharing.Model;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : Controller
{
    private readonly CarsharingContext _carsharingContext;

    public ClientController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody]RegistrationDto dto)
    {
        var isEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(dto.Email);
        var isName = new Regex(@"^[A-Z][a-zA-Z]*$").IsMatch(dto.Name);
        var isSurname = new Regex(@"^[A-Z][a-zA-Z]*$").IsMatch(dto.Surname);
        var isPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").IsMatch(dto.Password);
        
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

        var client = await _carsharingContext.Clients.FirstOrDefaultAsync(cl => cl.Email == dto.Email);
        if (client != null)
        {
            return BadRequest("A client with this email already exists");
        }

        var id = await _carsharingContext.Clients.CountAsync() + 1;
        var newClient =
            new Client
            {
                Id = id,
                Email = dto.Email,
                ClientInfo = new ClientInfo
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Age = dto.Age,
                    ClientId = id,
                    PassportType = "passport"
                },
                Password = dto.Password,
                RoleId = 1 //id for client
            };
        await _carsharingContext.AddAsync(newClient);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto dto)
    {
        var client = await _carsharingContext.Clients.FirstOrDefaultAsync(cl => cl.Email == dto.Email);
        if (client == null)
        {
            return Unauthorized("There is no client with this email");
        }

        if (client.Password != dto.Password)
        {
            return Unauthorized("Wrong password");
        }
        
        return Ok();
    }
}