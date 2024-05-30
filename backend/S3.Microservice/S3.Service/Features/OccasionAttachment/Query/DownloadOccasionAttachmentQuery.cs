using MediatR;
using MinioConsumer.Services;
using Results;

namespace MinioConsumer.Features.OccasionAttachment.Query;

public class DownloadOccasionAttachmentQuery : IRequest<Result<S3File>>
{
    public Guid AttachmentId { get; }

    public Guid ApplicantId { get; }

    public string AttachmentFileName { get; }

    public DownloadOccasionAttachmentQuery(Guid attachmentId, string attachmentFileName, Guid applicantId)
    {
        AttachmentId = attachmentId;
        AttachmentFileName = attachmentFileName;
        ApplicantId = applicantId;
    }
}
