using MediatR;

namespace MinioConsumer.Features.OccasionAttachment;

public class CreateOccasionAttachmentCommand : IRequest<HttpResponse<Guid>>
{
    public IEnumerable<IFormFile> Attachments { get; }

    public Guid OccasionUserId { get; }

    public Guid AttachmetByUser { get; }

    public CreateOccasionAttachmentCommand(Guid attachmentByUser, IEnumerable<IFormFile> attachments, Guid occasionUserId)
    {
        Attachments = attachments;
        OccasionUserId = occasionUserId;
        AttachmetByUser = attachmentByUser;
    }
}
