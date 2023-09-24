using Contracts;
using Services.Abstractions;
using File = System.IO.File;

namespace Carsharing.Services;

public class FileProvider : IFileProvider
{
    private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

    public async Task SaveAsync(string folder, IFile file)
    {
        folder = Path.Combine(CurrentDirectory, folder);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var filePath = Path.Combine(folder, file!.Name!);
        if (File.Exists(filePath))
            throw new InvalidOperationException($"File '{file!.Name!}' already exists!");
        await using var fs = File.Create(filePath);
        await file!.Content!.CopyToAsync(fs);
    }
    
    public void Delete(string fileFolder, string filename) 
        => File.Delete(Path.Combine(CurrentDirectory, fileFolder, filename));
}