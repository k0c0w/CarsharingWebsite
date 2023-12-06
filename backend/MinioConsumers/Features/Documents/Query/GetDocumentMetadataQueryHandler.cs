using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;

namespace MinioConsumer.Features.Documents.Query;

public class GetDocumentMetadataQueryHandler : IRequestHandler<GetDocumentMetadataQuery, HttpResponse<IEnumerable<DocumentMetadataDto>>>
{
    private IMetadataRepository<DocumentMetadata> metadataRepository;
    private ILogger<Exception> logger;

    public GetDocumentMetadataQueryHandler(IMetadataRepository<DocumentMetadata> metadataRepository, ILogger<Exception> _logger)
    {
        this.metadataRepository = metadataRepository;
        this.logger = _logger;
    }

    public async Task<HttpResponse<IEnumerable<DocumentMetadataDto>>> Handle(GetDocumentMetadataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<DocumentMetadataDto> results;

            if (request.IsAllDocumentsRequest)
            {
                var metadatas = await metadataRepository.GetAllAsync();

                results = request.IsAdminRequest ? metadatas.Select(MapToAdminDto) : metadatas.Select(MapToDto);
            }
            else
            {
                var metadata = await metadataRepository.GetByIdAsync(request.Id);

                if (metadata == null)
                    return new HttpResponse<IEnumerable<DocumentMetadataDto>>(System.Net.HttpStatusCode.NotFound, default, error: "Data not found.");

                results = request.IsAdminRequest ? new[] { MapToDto(metadata) } : new[] { MapToAdminDto(metadata) };  
            }

            return new HttpResponse<IEnumerable<DocumentMetadataDto>>(results);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new HttpResponse<IEnumerable<DocumentMetadataDto>>(System.Net.HttpStatusCode.InternalServerError, default, "An error occured while accessing data.");
        }
    }

    private DocumentMetadataDto MapToDto(DocumentMetadata metadata)
    => new()
    {
        CreationDate = metadata.CreationDateTimeUtc,
        DisplayableHeader = metadata.Annotation,
        Name = metadata.LinkedFileInfo.OriginalFileName,
        Url = GetPartialPath(metadata.Id)
    };

    private AdminDocumentMetadataDto MapToAdminDto(DocumentMetadata metadata)
    => new()
    {
        CreationDate = metadata.CreationDateTimeUtc,
        DisplayableHeader = metadata.Annotation,
        IsPrivate = !metadata.IsPublic,
        Name = metadata.LinkedFileInfo.OriginalFileName,
        Url = GetPartialPath(metadata.Id)
    };

    private string GetPartialPath(Guid Id) => $"/documents/{Id}/download";
}
