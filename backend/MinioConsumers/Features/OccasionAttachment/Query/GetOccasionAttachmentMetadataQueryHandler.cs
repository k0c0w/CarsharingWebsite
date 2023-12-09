using MediatR;
using MinioConsumer.Features.OccasionAttachment.Query.Dto;
using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;

namespace MinioConsumer.Features.OccasionAttachment.Query;

public class GetOccasionAttachmentMetadataQueryHandler : IRequestHandler<GetOccasionAttachmentMetadataQuery, HttpResponse<OccasionAttachmentInfoDto>>
{
    private IMetadataRepository<OccasionAttachmentMetadata> _metadataRepository;
    private ILogger<Exception> _logger;
    private string BaseUrl { get; }
    private HttpContext RequestContext { get; }

    public GetOccasionAttachmentMetadataQueryHandler(IHttpContextAccessor httpContextAccessor, IMetadataRepository<OccasionAttachmentMetadata> metadataRepository, ILogger<Exception> logger)
    {
        _metadataRepository = metadataRepository;
        _logger = logger;
        BaseUrl = httpContextAccessor!.HttpContext!.Request.Host.Value;//.Replace("", "localhost");
        RequestContext = httpContextAccessor.HttpContext!;
    }

    public async Task<HttpResponse<OccasionAttachmentInfoDto>> Handle(GetOccasionAttachmentMetadataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await _metadataRepository.GetByIdAsync(request.MetadataId);
            if (metadata == null)
                return new HttpResponse<OccasionAttachmentInfoDto>(System.Net.HttpStatusCode.NotFound, default);

            if (!(RequestContext.User.IsInRole("Manager") || RequestContext.User.IsInRole("Admin")))
            {
                if (!(metadata.AccessUserList.Contains(request.Applicant) || metadata.AttachmentAuthorId == request.Applicant))
                    return new HttpResponse<OccasionAttachmentInfoDto>(System.Net.HttpStatusCode.NotFound, default);
            }

            return new HttpResponse<OccasionAttachmentInfoDto>(new OccasionAttachmentInfoDto()
            {
                AttachmentsUrls = metadata.LinkedFileInfos.Select(x => GetDownloadPath(metadata.Id, x.ObjectName)).ToArray(),
                CreationDateUtc = metadata.CreationDateTimeUtc,
                UploaderId = metadata.AttachmentAuthorId
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new HttpResponse<OccasionAttachmentInfoDto>(System.Net.HttpStatusCode.InternalServerError, default, "An error occured while accessing data.");
        }
    }

    private string GetDownloadPath(Guid metadataId, string s3objectName) => $"{BaseUrl}/attachments/{metadataId}/{s3objectName}/download";
}
