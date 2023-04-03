namespace Carsharing.Forms;

public class RegistrationDto : LoginDto
{
    public string? RetryPassword { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    
    public DateTime Birthday { get; set; }
}