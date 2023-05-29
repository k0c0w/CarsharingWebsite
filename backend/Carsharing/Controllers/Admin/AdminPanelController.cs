using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Controllers.Admin;


[Route("admin")]
[Authorize(Roles = "Admin, Manager")]
public class AdminPanelController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    
    public AdminPanelController(IWebHostEnvironment env) => _env = env;
    
    [HttpGet("UI")]
    public IActionResult GetDashbordSPA()
    {
        var filePath = Path.Combine(_env.ContentRootPath, "AdminPanelResources", "index.html");
        return PhysicalFile(filePath, "text/html");
    }
}