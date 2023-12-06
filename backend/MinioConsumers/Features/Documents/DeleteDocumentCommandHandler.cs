using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;

namespace MinioConsumer.Features.Documents;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, HttpResponse>
{
    private readonly IS3Service _s3Service;
    private readonly IMetadataRepository<DocumentMetadata> _metadataRepository;
    private readonly ILogger<Exception> _logger;

    public DeleteDocumentCommandHandler(IS3Service s3Service, IMetadataRepository<DocumentMetadata> metadataRepository, ILogger<Exception> logger)
    {
        _metadataRepository = metadataRepository;
        _s3Service = s3Service;
        _logger = logger;
    }

    public async Task<HttpResponse> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await _metadataRepository.GetByIdAsync(request.Id);
            if (metadata == null)
                return new HttpResponse(System.Net.HttpStatusCode.NotFound, "Document was not found.");

            await _s3Service.RemoveFileFromBucketAsync(metadata.LinkedFileInfo.TargetBucketName, metadata.LinkedFileInfo.ObjectName)
                   .ContinueWith((t) => _metadataRepository.RemoveByIdAsync(request.Id), TaskContinuationOptions.OnlyOnRanToCompletion);

            return new HttpResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return new HttpResponse(System.Net.HttpStatusCode.InternalServerError, "Failure during deletion.");
        }
    }
}
