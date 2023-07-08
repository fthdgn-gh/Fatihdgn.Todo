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
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Alerts;
using System.Resources;

namespace Fatihdgn.Todo.App.ViewModels;

public class RegisterViewModel : BindableObject
{
    private readonly IFatihdgnTodoAuthClient _authClient;
    private string email;

    public string Email
    {
        get { return email; }
        set { email = value; OnPropertyChanged(); UpdateEmailError(); }
    }

    private void UpdateEmailError()
    {
        if (string.IsNullOrEmpty(Email))
        {
            EmailError = "Email address is empty";
            return;
        }
        if (!new EmailAddressAttribute().IsValid(Email))
        {
            EmailError = "Email address is not valid";
            return;
        }

        EmailError = string.Empty;
    }

    public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
    private string emailError;
    public string EmailError
    {
        get => emailError;
        set { emailError = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasEmailError)); }
    }

    private string password;

    public string Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); UpdatePasswordError(); }
    }

    private void UpdatePasswordError()
    {
        if (string.IsNullOrEmpty(Password))
        {
            PasswordError = "Password is empty";
            return;
        }

        PasswordError = string.Empty;
    }

    public bool HasPasswordError => !string.IsNullOrEmpty(PasswordError);

    private string passwordError;
    public string PasswordError
    {
        get => passwordError;
        set { passwordError = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasPasswordError)); }
    }



    private string confirmPassword;
    public string ConfirmPassword
    {
        get { return confirmPassword; }
        set { confirmPassword = value; OnPropertyChanged(); UpdateConfirmPasswordError(); }
    }

    private void UpdateConfirmPasswordError()
    {
        if (string.IsNullOrEmpty(ConfirmPassword))
        {
            ConfirmPasswordError = "Confirm password is empty";
            return;
        }

        if (Password != ConfirmPassword)
        {
            ConfirmPasswordError = "Confirm password doesn't match with the password";
            return;
        }

        ConfirmPasswordError = string.Empty;
    }

    public bool HasConfirmPasswordError => !string.IsNullOrEmpty(ConfirmPasswordError);

    private string confirmPasswordError;
    public string ConfirmPasswordError
    {
        get => confirmPasswordError;
        set { confirmPasswordError = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasConfirmPasswordError)); }
    }


    public bool HasError => !string.IsNullOrEmpty(Error);

    private string error;
    public string Error
    {
        get => error;
        set { error = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasError)); }
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
        if (!string.IsNullOrEmpty(EmailError) || !string.IsNullOrEmpty(PasswordError) || !string.IsNullOrEmpty(ConfirmPasswordError)) return;

        try
        {
            await _authClient.RegisterAsync(new AuthRegisterDTO { Email = Email, Password = Password, ConfirmPassword = ConfirmPassword });
            Error = string.Empty;
            var modalPage = new ContentPage
            {
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 20,
                    Padding = 20,
                    Children =
            {
                new Label
                {
                    Text = "You've successfully registered. You can login now.",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                },
                new Button
                {
                    Text = "OK",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Command = new Command(() =>
                    {
                        // Dismiss the modal when the button is clicked
                        Application.Current.MainPage.Navigation.PopModalAsync();
                    })
                }
            }
                }
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(modalPage);
            await Shell.Current.GoToAsync($"///{nameof(Login)}");
        }
        catch (ApiException ex)
        {
            var sb = new StringBuilder();
            if (ex is ApiException<object> e)
            {
                if (e.Result is JArray errors)
                {
                    foreach (var error in errors)
                    {
                        var description = error["description"];
                        sb.AppendLine(description.ToString());
                    }
                }

            }
            Error = sb.ToString();
        }
        catch
        {
            Error = "Unknown error. Please reach out to the service provider.";
        }

    }
}
