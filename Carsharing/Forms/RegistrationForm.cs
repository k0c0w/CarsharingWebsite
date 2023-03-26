namespace Carsharing.Forms;

public class RegistrationForm : LoginForm
{
    public string? RetryPassword { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
}