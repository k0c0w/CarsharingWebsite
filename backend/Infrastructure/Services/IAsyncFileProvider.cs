namespace Services;

public interface IAsyncFileProvider
{
    public Task SaveAsync(string folder, File file);

    public void Delete(string fileFolder, string filename);
}