using System.ComponentModel.DataAnnotations;

namespace Seedium.Models.DTO;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "The email address is required")]
    [RegularExpression(
        @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$",
        ErrorMessage = "Invalid Email Address"
    )]
    public string Email { get; set; }

    [Required(ErrorMessage = "The password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }
}
