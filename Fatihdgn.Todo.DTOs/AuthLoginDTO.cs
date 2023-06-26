using System.ComponentModel.DataAnnotations;

namespace Fatihdgn.Todo.DTOs;
public class AuthLoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}
