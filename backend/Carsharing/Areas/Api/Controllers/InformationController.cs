using System.Globalization;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

[Area("Api")]
public class InformationController : Controller
{
    public InformationController(CarsharingContext context) => _context = context;
    private readonly CarsharingContext _context;
    
    //todo: инкапсулировать в сервис
    [HttpGet]
    public async Task<IActionResult> Documents()
    {
        var documents = await _context.WebsiteDocuments.ToArrayAsync();

        return Json(documents.Select(x => new
        {
            name = x.Name,
            url = $"../documents/{x.FileName}"
        }));
    }

    [HttpGet]
    public async Task<IActionResult> News([FromQuery] int page, [FromQuery] int limit = 20, 
        [FromQuery] bool byDescending = true)
    {
        if (page <= 0 || limit <= 0) return Json(Array.Empty<object>());
        IQueryable<Entities.Model.Post> query = byDescending ? 
        _context.News.OrderByDescending(x => x.CreatedAt) : _context.News;
        var news = await query.Skip((page - 1) * limit).Take(limit).ToArrayAsync();
            
        return Json(news.Select(x => new
        {
            title = x.Title, body = x.Body,
            created_at = x.CreatedAt.ToString("D", CultureInfo.CreateSpecificCulture("ru-RU"))
        }));
    }
}