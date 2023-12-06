using MediatR;

namespace MinioConsumer.Features.Documents;

public class CreateDocumentCommand : IRequest<HttpResponse>
{
    public CreateDocumentCommand(bool isPrivate, string annotationToFile, Guid author, IFormFile formFile)
    {
        IsPrivate = isPrivate;
        UserId = author;
        File = formFile;
        AnnotationToFile = annotationToFile;
    }

    public bool IsPrivate { get; }

    public Guid UserId { get; }

    public string AnnotationToFile { get; }

    public IFormFile? File { get; }
}
