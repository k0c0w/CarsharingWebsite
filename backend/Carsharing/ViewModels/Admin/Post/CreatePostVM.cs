using System.ComponentModel.DataAnnotations;

namespace Carsharing.ViewModels.Admin.Post;

public class CreatePostVM
{
    [Required(ErrorMessage = "Введите заглавие новости")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Введите заглавие новости")]
    public string Body { get; set; }
}