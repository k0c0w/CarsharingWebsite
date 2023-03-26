﻿using System.Text.RegularExpressions;
using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Carsharing.Model;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : Controller
{
    private CarsharingContext _carsharingContext;

    public ClientController(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> RegistrationDto([FromBody]RegistrationForm form)
    {
        var isEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(form.Email);
        var isName = new Regex(@"^[A-Z][a-zA-Z]*$").IsMatch(form.Name);
        var isSurname = new Regex(@"^[A-Z][a-zA-Z]*$").IsMatch(form.Surname);
        var isPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").IsMatch(form.Password);
        
        if (!isEmail)
            return BadRequest("Wrong email format");
        
        if (!isPassword)
            return BadRequest("Wrong password format");
        
        if (!isSurname)
            return BadRequest("Wrong surname format");
        
        if (!isName) 
            return BadRequest("Wrong name format");
        
        if (form.Password != form.RetryPassword)
        {
            return BadRequest("Wrong confirm password");
        }

        var client = await _carsharingContext.Clients.FirstOrDefaultAsync(cl => cl.Email == form.Email);
        if (client != null)
        {
            return BadRequest("A client with this email already exists");
        }

        var id = await _carsharingContext.Clients.CountAsync() + 1;
        var newClient =
            new Client
            {
                Id = id,
                Email = form.Email,
                ClientInfo = new ClientInfo
                {
                    Name = form.Name,
                    Surname = form.Surname,
                    Age = form.Age,
                    ClientId = id,
                    PassportType = "passport"
                },
                Password = form.Password,
                RoleId = 1 //id for client
            };
        await _carsharingContext.AddAsync(newClient);
        await _carsharingContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginDto([FromBody]LoginForm form)
    {
        var client = await _carsharingContext.Clients.FirstOrDefaultAsync(cl => cl.Email == form.Email);
        if (client == null)
        {
            return Unauthorized("There is no client with this email");
        }

        if (client.Password != form.Password)
        {
            return Unauthorized("Wrong password");
        }
        
        return Ok();
    }
}