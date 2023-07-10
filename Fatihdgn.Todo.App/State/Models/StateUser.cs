namespace Fatihdgn.Todo.App.State.Models;

public class StateUser : BindableObject
{
    private string email;
    private string accessToken;
    private string refreshToken;

    public string Email
    {
        get => email;
        set
        {
            email = value;
            OnPropertyChanged();
        }
    }

    public string AccessToken
    {
        get => accessToken;
        set
        {
            accessToken = value;
            OnPropertyChanged();
        }
    }

    public string RefreshToken
    {
        get => refreshToken;
        set
        {
            refreshToken = value;
            OnPropertyChanged();
        }
    }
}
