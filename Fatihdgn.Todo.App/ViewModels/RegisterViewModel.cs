using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Pages;
using Fatihdgn.Todo.App.Providers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fatihdgn.Todo.App.ViewModels;

public class RegisterViewModel : BindableObject
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

    private string confirmPassword;

    public string ConfirmPassword
    {
        get { return confirmPassword; }
        set { confirmPassword = value; OnPropertyChanged(); }
    }

    private string error;
    public string Error
    {
        get => error;
        set { error = value; OnPropertyChanged(); }
    }


    public ICommand LoginCommand { get; private set; }
    public ICommand RegisterCommand { get; private set; }

    public RegisterViewModel()
    {
        _authClient = FatihdgnTodoAuthClientProvider.Current;
        RegisterCommand = new Command(async () => await RegisterAsync());
    }

    private async Task RegisterAsync()
    {
        var errors = new List<string>();

        if(string.IsNullOrEmpty(Email))
        {
            errors.Add("Email address is empty");
        }
        if (!new EmailAddressAttribute().IsValid(Email))
        {
            errors.Add("Email address is not valid");
        }
        if (string.IsNullOrEmpty(Password))
        {
            errors.Add("Password is empty");
        }
        if (string.IsNullOrEmpty(ConfirmPassword))
        {
            errors.Add("Confirm password is empty");
        }

        if(errors.Count > 0)
        {
            Error = string.Join("\n", errors);
            return;
        }


        try
        {
            await _authClient.RegisterAsync(new AuthRegisterDTO { Email = Email, Password = Password, ConfirmPassword = ConfirmPassword });
            Error = string.Empty;
        }
        catch (ApiException ex)
        {
            if (ex is ApiException<object> e)
            {
                var result = e.Result as dynamic;
                if (result["errors"] != null)
                {
                    var err = result["errors"] as JToken;
                    foreach(var error in err)
                    {

                    }
                }
                Error = Error.Trim();

            }
        }
        catch
        {
            Error = "Unknown error. Please reach out the service provider.";
        }

    }
}
