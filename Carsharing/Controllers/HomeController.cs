using Microsoft.AspNetCore.Mvc;
using Carsharing.Model;

namespace Carsharing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]

        public IActionResult Test()
        {
            return Ok("Hi");
        } 
                
        }
    }

