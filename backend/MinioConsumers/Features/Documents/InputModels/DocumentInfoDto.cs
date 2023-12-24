using System.ComponentModel.DataAnnotations;

namespace MinioConsumer.Features.Documents.InputModels;

public class DocumentInfoDto
{
    public IEnumerable<string> AccessRoles { get; set; } = new string[] { "user" };

    [Required]
    public bool IsPrivate { get; set; }

    [Required]
    public string Annotation { get; set; }
}
