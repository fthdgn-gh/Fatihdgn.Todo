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
using Fatihdgn.Todo.App.Helpers;
using CommunityToolkit.Mvvm.Input;

namespace Fatihdgn.Todo.App.ViewModels;

public partial class RegisterViewModel : BindableObject
{
    private readonly IFatihdgnTodoAuthClient _authClient;

    public RegisterViewModel()
    {
        _authClient = FatihdgnTodoAuthClientProvider.Current;
        Setup();
    }

    public void Setup()
    {
        SetupEmailObject();
        SetupPasswordObject();
        SetupConfirmPasswordObject();
    }
    public ValidatableBindableObject<string> Email { get; set; }
    void SetupEmailObject()
    {
        Email = new ValidatableBindableObject<string>(string.Empty);
        Email.Validators.Add(new ObjectValidator<string>("Email address is empty", string.IsNullOrEmpty));
        Email.Validators.Add(new ObjectValidator<string>("Email address is not valid", value => !new EmailAddressAttribute().IsValid(value)));
    }

    public ValidatableBindableObject<string> Password { get; set; }
    void SetupPasswordObject()
    {
        Password = new ValidatableBindableObject<string>(string.Empty);
        Password.Validators.Add(new ObjectValidator<string>("Password is empty", string.IsNullOrEmpty));
    }

    public ValidatableBindableObject<string> ConfirmPassword { get; set; }
    void SetupConfirmPasswordObject()
    {
        ConfirmPassword = new ValidatableBindableObject<string>(string.Empty);
        ConfirmPassword.Validators.Add(new ObjectValidator<string>("Confirm password is empty", string.IsNullOrEmpty));
        ConfirmPassword.Validators.Add(new ObjectValidator<string>("Confirm password doesn't match with the password", value => Password.Value != value));
    }

    public bool HasError => !string.IsNullOrEmpty(Error);

    private string error;
    public string Error
    {
        get => error;
        set { error = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasError)); }
    }

    [RelayCommand]
    public async Task GoToLogin()
    {
        await Shell.Current.GoToAsync($"///{nameof(Login)}");
    }

    [RelayCommand]
    public async Task RegisterAsync()
    {
        if (Email.HasMessage || Password.HasMessage || ConfirmPassword.HasMessage) return;

        try
        {
            await _authClient.RegisterAsync(new AuthRegisterDTO { Email = Email.Value, Password = Password.Value, ConfirmPassword = ConfirmPassword.Value });
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
        Setup();
    }
}
