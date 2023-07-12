using Fatihdgn.Todo.App.ViewModels;

namespace Fatihdgn.Todo.App.Pages;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}