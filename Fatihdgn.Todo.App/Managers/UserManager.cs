namespace Fatihdgn.Todo.App.Managers;

public class UserManager
{
    private readonly IValueManager<string> _email;
    private readonly IValueManager<string> _accessToken;
    private readonly IValueManager<string> _refreshToken;

    public UserManager(IValueManager<string> email, IValueManager<string> accessToken, IValueManager<string> refreshToken)
    {
        _email = email;
        _accessToken = accessToken;
        _refreshToken = refreshToken;
    }

    public IValueManager<string> Email => _email;
    public IValueManager<string> AccessToken => _accessToken;
    public IValueManager<string> RefreshToken => _refreshToken;
}
