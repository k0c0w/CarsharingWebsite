namespace MinioConsumer.Features.Documents.InputModels;

public class DocumentInfoDto
{
    public IEnumerable<string> AccessRoles { get; set; } = new string[] { "user" };

    public bool IsPrivate { get; set; } 
}
