using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Pages;
using Fatihdgn.Todo.App.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fatihdgn.Todo.App.ViewModels;

public class LoginViewModel : BindableObject
{
    private readonly IFatihdgnTodoAuthClient _authClient;

    private string email;

    public string Email
    {
        get { return email; }
        set { email = value; OnPropertyChanged(); }
    }

    private string password;

    public string Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); }
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


    public ICommand LoginCommand { get; private set; }
    public ICommand RegisterCommand { get; private set; }

    public LoginViewModel()
    {
        _authClient = FatihdgnTodoAuthClientProvider.Current;
        LoginCommand = new Command(async () => await LoginAsync());
        RegisterCommand = new Command(async () => await RegisterAsync());
    }

    private async Task LoginAsync()
    {
        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
        {
            try
            {
                var response = await _authClient.LoginAsync(new AuthLoginDTO { Email = Email, Password = Password, RememberMe = RememberMe });
                
                await Shell.Current.GoToAsync($"///{nameof(Dashboard)}");
            }
            catch {
                Error = "Username or password isn't correct";
            }
        }
    }

    private async Task RegisterAsync()
    {
        await Shell.Current.GoToAsync($"///{nameof(Register)}");
    }
}
