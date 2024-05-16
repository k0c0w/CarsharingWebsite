namespace Services.Abstractions;

public record SendMessageDto(ChatInfoDto ChatInfo, string Text, string? SenderId = null);
