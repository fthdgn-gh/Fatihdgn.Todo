using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fatihdgn.Todo.App.ViewModels;

public class AuthViewModel : BindableObject
{
    private readonly IFatihdgnTodoAuthClient _authClient;

    private string username;

    public string Username
    {
        get { return username; }
        set { username = value; OnPropertyChanged(); }
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


    public string Error { get; set; } = "Username or password isn't correct";


    public ICommand LoginCommand { get; private set; }

    public AuthViewModel()
    {
        _authClient = FatihdgnTodoAuthClientProvider.Current;
        LoginCommand = new Command(async () => await LoginAsync());
    }

    private async Task LoginAsync()
    {
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
        {
            try
            {
                var response = await _authClient.LoginAsync(new AuthLoginDTO { Email = Username, Password = Password, RememberMe = true });
            }
            catch (ApiException ex) {
                Error = "Username or password isn't correct";
            }
        }
    }
}
