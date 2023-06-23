using System.ComponentModel.DataAnnotations;

namespace Fatihdgn.Todo.API.Models;

public class RefreshTokenModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}
