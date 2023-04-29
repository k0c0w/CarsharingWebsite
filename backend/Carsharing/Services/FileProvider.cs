using Microsoft.AspNetCore.Http;

namespace Carsharing.Services;

public class FileProvider : IAsyncFileProvider
{
    private string CurrentDirectory => Directory.GetCurrentDirectory();

    public async Task SaveAsync(string folder, File file)
    {
        folder = Path.Combine(CurrentDirectory, folder);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var filePath = Path.Combine(folder, file.FileName);
        if (System.IO.File.Exists(filePath))
            throw new InvalidOperationException($"File '{file.FileName}' already exists!");

        await using var fs = System.IO.File.Create(filePath);
        await file.FileContent.CopyToAsync(fs);
    }

    public void Delete(string fileFolder, string filename) 
        => System.IO.File.Delete(Path.Combine(CurrentDirectory, fileFolder, filename));
}