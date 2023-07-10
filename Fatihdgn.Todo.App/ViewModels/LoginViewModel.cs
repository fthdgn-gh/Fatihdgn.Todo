using CommunityToolkit.Mvvm.Input;
using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Helpers;
using Fatihdgn.Todo.App.Managers;
using Fatihdgn.Todo.App.Pages;
using Fatihdgn.Todo.App.Providers;
using Fatihdgn.Todo.App.State;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fatihdgn.Todo.App.ViewModels;

public partial class LoginViewModel : BindableObject
{
    private readonly IFatihdgnTodoAuthClient _authClient;

    public ValidatableBindableObject<string> Email { get; set; }
    void SetupEmailObject()
    {
        Email = new ValidatableBindableObject<string>(string.Empty);
        Email.Validators.Add(new ObjectValidator<string>("Email address is empty", string.IsNullOrEmpty));
        Email.Validators.Add(new ObjectValidator<string>("Email address is not valid", value => !new EmailAddressAttribute().IsValid(value)));
#if DEBUG
        Email.Value = "user@example.com";
#endif
    }
    public ValidatableBindableObject<string> Password { get; set; }
    void SetupPasswordObject()
    {
        Password = new ValidatableBindableObject<string>(string.Empty);
        Password.Validators.Add(new ObjectValidator<string>("Password is empty", string.IsNullOrEmpty));
#if DEBUG
        Password.Value = "Password1!";
#endif
    }

    void Setup()
    {
        SetupEmailObject();
        SetupPasswordObject();
    }

    private bool rememberMe;
    public bool RememberMe
    {
        get { return rememberMe; }
        set { rememberMe = value; OnPropertyChanged(); }
    }


    private string error;
    public string Error
    {
        get => error;
        set { error = value; OnPropertyChanged(); }
    }

    public LoginViewModel()
    {
        _authClient = FatihdgnTodoAuthClientProvider.Current;
        Setup();
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (Email.HasMessage || Password.HasMessage) return;
        try
        {
            var response = await _authClient.LoginAsync(new AuthLoginDTO { Email = Email.Value, Password = Password.Value, RememberMe = RememberMe });
            var accessToken = response.AccessToken;
            var refreshToken = response.RefreshToken;
            await SecureStorageUserManager.Instance.Email.SetAsync(Email.Value);
            await SecureStorageUserManager.Instance.AccessToken.SetAsync(accessToken);
            if (refreshToken is not null)
                await SecureStorageUserManager.Instance.AccessToken.SetAsync(refreshToken);
            FatihdgnTodoClientProvider.ApplyAccessToken(accessToken);
            await AppStatePopulator.Populate(AppState.Instance);

            await Shell.Current.GoToAsync($"///{nameof(Dashboard)}");
        }
        catch
        {
            Error = "Username or password isn't correct.";
        }
        Setup();
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        await Shell.Current.GoToAsync($"///{nameof(Register)}");
        Setup();
    }
}
