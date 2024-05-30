using MinioConsumer.Services;

namespace MinioConsumer.Models;

public record OccasionAttachmentMetadata : MetadataBase
{
    public string BucketName { get; } = KnownBuckets.OCCASIONATTACHMENTS;

    public List<Guid> AccessUserList {  get; set; }

    public Guid AttachmentAuthorId { get; set; }

    public OccasionAttachmentMetadata(Guid id,  Guid attachmentAuthorId, int targetFilesCount) : base(id, KnownBuckets.OCCASIONATTACHMENTS, targetFilesCount)
    {
        AttachmentAuthorId = attachmentAuthorId;
    }
}
