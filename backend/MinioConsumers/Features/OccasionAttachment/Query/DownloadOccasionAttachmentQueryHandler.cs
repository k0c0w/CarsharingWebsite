using MediatR;
using MinioConsumer.Models;
using MinioConsumer.Services;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using Shared.Results;

namespace MinioConsumer.Features.OccasionAttachment.Query;

public class DownloadOccasionAttachmentQueryHandler : IRequestHandler<DownloadOccasionAttachmentQuery, Result<S3File>>
{
    private readonly IMetadataRepository<OccasionAttachmentMetadata> _metadataRepository;
    private readonly IS3Service _s3Service;
    private readonly ILogger<Exception> _logger;
    private HttpContext RequestContext { get; }

    public DownloadOccasionAttachmentQueryHandler(IHttpContextAccessor contextAccessor,
        IMetadataRepository<OccasionAttachmentMetadata> metadataRepository, IS3Service s3Service, ILogger<Exception> logger)
    {
        _metadataRepository = metadataRepository;
        _s3Service = s3Service;
        _logger = logger;
        RequestContext = contextAccessor.HttpContext!;
    }

    public async Task<Result<S3File>> Handle(DownloadOccasionAttachmentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await _metadataRepository.GetByIdAsync(request.AttachmentId);

            if (metadata == null)
                return new Error<S3File>();
            //todo: 
            /*
            if (!(RequestContext.User.IsInRole("Manager") || RequestContext.User.IsInRole("Admin")))
            {
                if (!(metadata.AccessUserList.Contains(request.ApplicantId) || metadata.AttachmentAuthorId == request.ApplicantId))
                    return new Error<S3File>();
            }
            */

            var fileinfo = metadata.LinkedFileInfos.FirstOrDefault(x => x.ObjectName == request.AttachmentFileName);
            if (fileinfo == null)
                return new Error<S3File>();

            var file = await _s3Service.GetFileFromBucketAsync(fileinfo.TargetBucketName, fileinfo.ObjectName);
            return new Ok<S3File>(new S3File(fileinfo.OriginalFileName, file.BucketName, file.ContentStream, file.ContentType));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Error<S3File>();
        }
    }
}
