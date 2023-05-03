using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Post
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Title { get; set; } = String.Empty;

    public string Body { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }
}