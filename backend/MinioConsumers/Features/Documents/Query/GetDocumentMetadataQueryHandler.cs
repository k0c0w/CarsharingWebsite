using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using Shared.Results;

namespace MinioConsumer.Features.Documents.Query;

public class GetDocumentMetadataQueryHandler : IRequestHandler<GetDocumentMetadataQuery, HttpResponse<DocumentMetadataDto>>
{
    private IMetadataRepository<DocumentMetadata> metadataRepository;
    private ILogger<Exception> logger;

    public GetDocumentMetadataQueryHandler(IMetadataRepository<DocumentMetadata> metadataRepository, ILogger<Exception> _logger)
    {
        this.metadataRepository = metadataRepository;
        this.logger = _logger;
    }

    public async Task<HttpResponse<DocumentMetadataDto>> Handle(GetDocumentMetadataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await metadataRepository.GetByIdAsync(request.Id);

            if (metadata == null)
                return new HttpResponse<DocumentMetadataDto>(System.Net.HttpStatusCode.NotFound,default, error:"Data not found.");

            return new HttpResponse<DocumentMetadataDto>(new DocumentMetadataDto()
            {
                CreationDate = metadata.CreationDateTimeUtc,
                DisplayableHeader = metadata.Schema,
                Name = metadata.LinkedFileInfo.OriginalFileName,
                Url = $"/documents/{request.Id}/download"
            });
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new HttpResponse<DocumentMetadataDto>(System.Net.HttpStatusCode.InternalServerError, default, "An error occured while accessing data.");
        }
    }
}
