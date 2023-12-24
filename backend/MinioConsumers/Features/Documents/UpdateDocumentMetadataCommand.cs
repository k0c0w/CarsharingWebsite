using MediatR;

namespace MinioConsumer.Features.Documents;

public class UpdateDocumentMetadataCommand : IRequest<HttpResponse>
{
    public Guid Id { get; }

    public bool? IsPublic { get; }

    public string? Annotation { get; }

    public UpdateDocumentMetadataCommand(Guid id, bool? isPublic, string? annotation)
    {
        Id = id;
        IsPublic = isPublic;
        Annotation = annotation;
    }
}
