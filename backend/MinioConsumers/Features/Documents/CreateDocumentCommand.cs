using MediatR;

namespace MinioConsumer.Features.Documents;

public class CreateDocumentCommand : IRequest<HttpResponse>
{
    public CreateDocumentCommand(bool isPrivate, Guid author, IFormFile formFile)
    {
        IsPrivate = isPrivate;
        UserId = author;
        File = formFile;
    }

    public bool IsPrivate { get; }

    public Guid UserId { get; }

    public IFormFile? File { get; }
}
