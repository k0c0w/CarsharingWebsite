using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Post
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Body { get; set; }
    
    public DateTime CreatedAt { get; set; }
}