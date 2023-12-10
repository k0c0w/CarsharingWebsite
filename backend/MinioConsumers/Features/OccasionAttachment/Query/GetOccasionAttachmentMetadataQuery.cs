using MediatR;
using MinioConsumer.Features.OccasionAttachment.Query.Dto;

namespace MinioConsumer.Features.OccasionAttachment.Query;

public class GetOccasionAttachmentMetadataQuery : IRequest<HttpResponse<OccasionAttachmentInfoDto>>
{
    public Guid MetadataId { get; }

    public Guid Applicant {  get; }

    public GetOccasionAttachmentMetadataQuery(Guid metadataId, Guid applicant)
    {
        MetadataId = metadataId;
        Applicant = applicant;
    }
}
