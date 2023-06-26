namespace Fatihdgn.Todo.DTOs;

public record struct AuthLoginResponseDTO(string AccessToken, string? RefreshToken = null);
