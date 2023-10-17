using System.Globalization;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Carsharing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformationController : ControllerBase
{
    public InformationController(CarsharingContext context) => _context = context;
    private readonly CarsharingContext _context;
    
    [HttpGet("[action]")]
    public async Task<IActionResult> Documents()
    {
        var documents = await _context.WebsiteDocuments.ToArrayAsync();

        return new JsonResult(documents.Select(x => new
        {
            name = x.Name,
            url = $"../documents/{x.FileName}"
        }));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> News([FromQuery] int page = 1, [FromQuery] int limit = 20, 
        [FromQuery] bool byDescending = true)
    {
        if (page <= 0 || limit <= 0) return new JsonResult(Array.Empty<object>());
        IQueryable<Domain.Entities.Post> query = byDescending ?
            _context.News.OrderByDescending(x => x.CreatedAt) : _context.News;
        var news = await query.Skip((page - 1) * limit).Take(limit).ToArrayAsync();
            
        return new JsonResult(news.Select(x => new { title = x.Title, body = x.Body, created_at = x.CreatedAt.ToString("D", CultureInfo.CreateSpecificCulture("ru-RU")) }));
    }
}