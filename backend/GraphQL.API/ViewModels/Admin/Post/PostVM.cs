namespace GraphQL.API.ViewModels.Admin.Post;

public class PostVM
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public DateTime CreatedAt { get; set; }
}