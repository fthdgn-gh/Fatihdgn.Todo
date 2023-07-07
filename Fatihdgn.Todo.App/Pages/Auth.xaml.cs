using Fatihdgn.Todo.App.ViewModels;

namespace Fatihdgn.Todo.App.Pages;

public partial class Auth : ContentPage
{
	public Auth()
	{
		InitializeComponent();
		BindingContext = new AuthViewModel();
	}
}