namespace Carsharing.Services;

public record File(string FileName, Stream FileContent, long Length);
