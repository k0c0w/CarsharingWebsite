using System.ComponentModel.DataAnnotations;

namespace Carsharing.ViewModels.Admin.Post;

public class EditPostVM
{
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = "No content";

    [Required(AllowEmptyStrings = false)]
    public string Body { get; set; } = "No content";
}