using Carsharing.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Controllers;

[Area("Admin")]
public class FileController : Controller
{
    private readonly IAsyncFileProvider _fileProvider;
    
    private const string WebRoot = "wwwroot";

    public FileController(IAsyncFileProvider fileProvider) => _fileProvider = fileProvider;

    //вообще не безопасно, поменять или проверять экстенш файла и фолдер
    [HttpPost]
    public async Task<IActionResult> Save(IFormFile file)
    {
        var folder = "tariffs";
        await using var read = file.OpenReadStream();
        var f = new Carsharing.Services.File(file.FileName, read, file.Length);
        await _fileProvider.SaveAsync(Path.Combine(WebRoot, folder), f);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] string folderName, [FromQuery] string fileName)
    {
        _fileProvider.Delete(Path.Combine(WebRoot, folderName), fileName);
        return Ok();
    }
}