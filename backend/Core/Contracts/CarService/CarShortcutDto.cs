namespace Contracts;

public record CarShortcutDto
{
    public string? Brand { get; set; }
    
    public string? Model { get; set; }
    
    public bool IsOpened { get; set; }
    
    public int Id { get; set; }

    public string? LicensePlate { get; set; }
    
    public string? Image { get; init; }
}