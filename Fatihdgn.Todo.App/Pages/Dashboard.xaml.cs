using Fatihdgn.Todo.App.ViewModels;

namespace Fatihdgn.Todo.App.Pages;

public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel();
    }
}