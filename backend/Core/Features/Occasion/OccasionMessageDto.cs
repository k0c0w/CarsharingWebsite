namespace Features.Occasion;

public class OccasionMessageDto
{
    public Guid Id { get; init; }

    public string MessageText { get; init; }

    public string AuthorName { get; init; }
    
    public bool IsFromManager { get; init; }

    public IEnumerable<OccasionMessageAttachmentDto> Attachments { get; set; }
}


public class OccasionMessageAttachmentDto
{
    public string DownloadUrl { get; init; }

    public string ContentType { get; init; }
}