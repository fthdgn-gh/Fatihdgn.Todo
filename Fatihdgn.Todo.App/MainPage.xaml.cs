using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Managers;
using Fatihdgn.Todo.App.Pages;
using Fatihdgn.Todo.App.Providers;
using Fatihdgn.Todo.App.State;
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
#if !DEBUG
        var userManager = SecureStorageUserManager.Instance;
        var accessToken = await userManager.AccessToken.GetAsync();
        var refreshToken = await userManager.RefreshToken.GetAsync();
        var email = await userManager.Email.GetAsync();

        if (email is null)
        {
            await Shell.Current.GoToAsync($"///{nameof(Login)}");
            return;
        }

        if (accessToken is not null)
        {
            FatihdgnTodoClientProvider.ApplyAccessToken(accessToken);
            await AppStatePopulator.Populate(AppState.Instance);
            await Shell.Current.GoToAsync($"///{nameof(Dashboard)}");

            return;
        }

        if (refreshToken is not null)
        {

            var refreshTokenResponse = await _auth.RefreshTokenAsync(new AuthRefreshTokenDTO { Email = email, RefreshToken = refreshToken });
            accessToken = refreshTokenResponse.AccessToken;
            refreshToken = refreshTokenResponse.RefreshToken;
            await SecureStorageUserManager.Instance.AccessToken.SetAsync(accessToken);
            if (refreshToken is not null)
                await SecureStorageUserManager.Instance.AccessToken.SetAsync(refreshToken);
            FatihdgnTodoClientProvider.ApplyAccessToken(accessToken);
            await AppStatePopulator.Populate(AppState.Instance);
            await Shell.Current.GoToAsync($"///{nameof(Dashboard)}");
            return;
        }
#endif
        await Shell.Current.GoToAsync($"///{nameof(Login)}");
    }


}