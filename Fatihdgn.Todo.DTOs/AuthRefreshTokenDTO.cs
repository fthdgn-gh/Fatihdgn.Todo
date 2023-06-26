using System.ComponentModel.DataAnnotations;

namespace Fatihdgn.Todo.DTOs;

public class AuthRefreshTokenDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}
