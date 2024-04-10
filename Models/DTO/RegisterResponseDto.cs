namespace Seedium.Models.DTO;

public class RegisterResponseDto
{
    public string Email { get; set; }

    public string Token { get; set; }

    public List<string> Roles { get; set; }
}
