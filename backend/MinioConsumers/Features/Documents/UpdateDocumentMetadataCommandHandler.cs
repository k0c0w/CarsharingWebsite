using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;

namespace MinioConsumer.Features.Documents;

public class UpdateDocumentMetadataCommandHandler : IRequestHandler<UpdateDocumentMetadataCommand, HttpResponse>
{
    private readonly IMetadataRepository<DocumentMetadata> _metadataRepository;
    private readonly ILogger<Exception> _logger;

    public UpdateDocumentMetadataCommandHandler(IMetadataRepository<DocumentMetadata> metadataRepository, ILogger<Exception> logger)
    {
        _metadataRepository = metadataRepository;
        _logger = logger;
    }

    public async Task<HttpResponse> Handle(UpdateDocumentMetadataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await _metadataRepository.GetByIdAsync(request.Id);

            if (metadata == null)
            {
                return new HttpResponse(System.Net.HttpStatusCode.NotFound);
            }

            if (request.IsPublic.HasValue)
                metadata.IsPublic = request.IsPublic.Value;

            if (request.Annotation != null)
                metadata.Annotation = request.Annotation;

            await _metadataRepository.UpdateAsync(metadata);

            return new HttpResponse(System.Net.HttpStatusCode.NoContent);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new HttpResponse(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
