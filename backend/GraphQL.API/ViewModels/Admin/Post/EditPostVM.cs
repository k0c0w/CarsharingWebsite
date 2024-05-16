using System.ComponentModel.DataAnnotations;

namespace GraphQL.API.ViewModels.Admin.Post;

public class EditPostVM
{
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = "No content";

    [Required(AllowEmptyStrings = false)]
    public string Body { get; set; } = "No content";
}