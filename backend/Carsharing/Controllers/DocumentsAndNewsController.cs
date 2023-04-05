using System.Globalization;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;

public class DocumentsAndNewsController : Controller
{
    public DocumentsAndNewsController(CarsharingContext context) => _context = context;
    private readonly CarsharingContext _context;
    
    //todo: инкапсулировать в сервис
    [HttpGet("/document")]
    public async Task<IActionResult> GetDocumentsDescriptions()
    {
        var documents = await _context.WebsiteDocuments.ToArrayAsync();

        return Json(documents.Select(x => new
        {
            name = x.Name,
            url = $"../documents/{x.FileName}"
        }));
    }

    [HttpGet("/news")]
    public async Task<IActionResult> GetNews([FromQuery] int page, [FromQuery] int limit = 20, 
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