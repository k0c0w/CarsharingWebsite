namespace GraphQL.API.ViewModels;

public class EditUserVm
{
    public string? LastName { get; set; } 
    
    public string? FirstName { get; set; }

    public string? Email { get; set; }
    
    public DateTime BirthDay { get; set; }
    
    public string? Passport { get; set; }
    
    public int? DriverLicense { get; set; } 
}