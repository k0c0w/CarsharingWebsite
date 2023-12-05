using MediatR;
using Shared.Results;

namespace MinioConsumer.Features.Documents.Query;

public class GetDocumentMetadataQuery : IRequest<HttpResponse<DocumentMetadataDto>>
{
    public Guid Id { get; }

    public GetDocumentMetadataQuery(Guid id) => Id = id;
}
