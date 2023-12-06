using MediatR;

namespace MinioConsumer.Features.Documents.Query;

public class GetDocumentMetadataQuery : IRequest<HttpResponse<IEnumerable<DocumentMetadataDto>>>
{
    public Guid Id { get; }

    public bool IsAdminRequest { get; }

    public GetDocumentMetadataQuery(Guid id = default, bool adminRequest = false)
    {
        Id = id;
        IsAdminRequest = adminRequest;
    }

    public bool IsAllDocumentsRequest => Id == default;
}
