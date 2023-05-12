using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Carsharing.Forms;


public class LoginDto
{
    [RegExCheck(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Wrong email format")]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}