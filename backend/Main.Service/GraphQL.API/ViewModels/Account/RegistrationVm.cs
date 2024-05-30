namespace GraphQL.API.ViewModels.Account;

public class RegistrationVm : LoginVM
{
    public RegistrationVm(string? accept, DateTime birthdate, string name, string surname, string password, string email) 
    : base(password, email)
    {
        Accept = accept;
        Surname = surname;
        Name = name;
        Birthdate = birthdate;
    }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    
    public DateTime Birthdate { get; set; }
    
    public string? Accept { get; set; }
}
