namespace Features.History;

public record MessageDto
{
    public string Text { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public bool IsFromManager { get; set; }

    public DateTime Time { get; set; }

    public string MessageId { get; set; } = string.Empty;
}
