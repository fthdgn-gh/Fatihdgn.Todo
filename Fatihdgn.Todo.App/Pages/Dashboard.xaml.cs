using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Extensions;
using Fatihdgn.Todo.App.ViewModels;

namespace Fatihdgn.Todo.App.Pages;

public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel();
    }

    private DashboardViewModel VM => BindingContext as DashboardViewModel;

    private async void TodoItem_IsCompletedChanged(object sender, CheckedChangedEventArgs e)
    {
        var item = TagExtension.GetTag(sender as BindableObject) as TodoItemDTO;
        var isCompleted = e.Value;
        await VM.TodoItemIsCompletedChangedAsync(item, isCompleted);
    }
}