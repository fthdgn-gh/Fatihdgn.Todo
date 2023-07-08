using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Managers;
using Fatihdgn.Todo.App.Pages;
using System.Net.Http;

namespace Fatihdgn.Todo.App;

public partial class MainPage : ContentPage
{
    private readonly IFatihdgnTodoAuthClient _auth;
    const string ApiEndpoint = "https://localhost:32768";

    public MainPage()
    {
        InitializeComponent();
        _auth = new FatihdgnTodoAuthClient(ApiEndpoint, new HttpClient());
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        
        var accessToken = await SecureStorageUserManager.Instance.AccessToken.GetAsync();
        var refreshToken = await SecureStorageUserManager.Instance.RefreshToken.GetAsync();
        var email = await SecureStorageUserManager.Instance.Email.GetAsync();

        if (email is null)
        {
            await Shell.Current.GoToAsync($"///{nameof(Login)}");
            return;
        }

        if (accessToken is not null)
        {
            await Shell.Current.GoToAsync($"///{nameof(Dashboard)}");
            return;
        }

        if (refreshToken is not null)
        {

            var refreshTokenResponse = await _auth.RefreshTokenAsync(new AuthRefreshTokenDTO { Email = email, RefreshToken = refreshToken });
            accessToken = refreshTokenResponse.AccessToken;
            refreshToken = refreshTokenResponse.RefreshToken;
            await SecureStorageUserManager.Instance.AccessToken.SetAsync(accessToken);
            await SecureStorageUserManager.Instance.AccessToken.SetAsync(refreshToken);
            await Shell.Current.GoToAsync($"///{nameof(Dashboard)}");
            return;
        }
        await Shell.Current.GoToAsync($"///{nameof(Login)}");
    }
}