using System.Security.Claims;

namespace Fatihdgn.Todo.API.Extensions;

public static class ClaimsExtensions
{
    public static string? GetNameIdentifier(this ClaimsPrincipal self) => self.FindFirstValue(ClaimTypes.NameIdentifier);
}
