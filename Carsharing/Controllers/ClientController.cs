using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Carsharing.Model;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : Controller
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Registration([FromBody]RegistrationForm form)
    {
        if (form.Password != form.RetryPassword)
        {
            return BadRequest("Wrong confirm password");
        }
        await using (var context = new CarsharingContext())
        {
            var client = await context.Clients.FirstOrDefaultAsync(cl => cl.Email == form.Email);
            if (client != null)
            {
                return BadRequest("A client with this email already exists");
            }
            var id = await context.Clients.CountAsync() + 1;
            var newClient = 
                new Client
                {
                    Id = id,
                    Email = form.Email!, 
                    ClientInfo = new ClientInfo
                    {
                        Name = form.Name!, 
                        Surname = form.Surname!, 
                        Age = form.Age,
                        ClientId = id, 
                        PassportType = "passport"
                    },
                    Password = form.Password!,
                    RoleId = 1 //id for client
                };
            await context.AddAsync(newClient);
            await context.SaveChangesAsync();
        }
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]LoginForm form)
    {
        await using (var context = new CarsharingContext())
        {
            var client = await context.Clients.FirstOrDefaultAsync(cl => cl.Email == form.Email);
            if (client == null)
            {
                return Unauthorized("There is no client with this email");
            }
            if (client.Password != form.Password)
            {
                return Unauthorized("Wrong password");
            }
        }
        return Ok();
    }
}