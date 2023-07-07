using Fatihdgn.Todo.App.ViewModels;

namespace Fatihdgn.Todo.App.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel();
	}
}