namespace Domain;

public class User
{
    public static User Anonymous = new User()
    {
        Id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
        IsManager = false,
        Name = "anonymous"
    };

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsManager { get; set; }
}
