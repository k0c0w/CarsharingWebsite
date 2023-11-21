using System.Globalization;
using Features.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Carsharing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformationController : ControllerBase
{
    private readonly CarsharingContext _context;
    private readonly ISender _sender;

    public InformationController(CarsharingContext context, ISender sender)
    {
        _context= context;
        _sender = sender;
    }

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

        var newsQuery = await _sender.Send(new GetPaginatedPostsQuery()
        {
            Page = page,
            Limit = limit,
            ByDescending = byDescending
        });

        if (!newsQuery)
            return BadRequest();


        return new JsonResult(newsQuery.Value!.Select(x => new { title = x.Title, body = x.Body, created_at = x.CreatedAt.ToString("D", CultureInfo.CreateSpecificCulture("ru-RU")) }));
    }
}