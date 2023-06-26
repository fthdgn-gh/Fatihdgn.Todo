using System.Security.Cryptography;

namespace Fatihdgn.Todo.Entities.Extensions;

public static class TodoUserEntityExtensions
{
    public static TodoUserEntity RenewRefreshToken(this TodoUserEntity self)
    {
        if (self == null) throw new NullReferenceException(nameof(self));

        var randomNumber = new byte[128];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        self.RefreshToken = Convert.ToBase64String(randomNumber);
        return self;
    }
}
