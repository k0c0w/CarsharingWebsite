using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Controllers;

[Area("Api")]
public class ProfileController : Controller
{
    
    [HttpGet]
    public IActionResult Index()
    {
        return Json(new 
        {
            rented_cars = new object[]
            {
                new { model="Sonata", license_plate="H132OP116" },
                new { model="Sonata", license_plate="H133OP116" },
                new { model="Highlander", license_plate="H135OP116" }
            },
            user_info = new
            {
                balance=23459.05f,
                email="art.kazan@mail.ru",
                full_name="Василий Пупкин"
            }
        });
    }

    [HttpGet]
    public IActionResult PersonalInfo()
    {
        return Json(new
        {
            email="art.kazan@mail.ru",
            name="Василий",
            surname="Пупкин",
            age=25,
            passport="9217 181511",
        });
    }
}