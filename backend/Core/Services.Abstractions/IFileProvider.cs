
using Contracts;

namespace Services.Abstractions;

public interface IFileProvider
{
    public Task SaveAsync(string folder, IFile file);

    public void Delete(string fileFolder, string filename);
}