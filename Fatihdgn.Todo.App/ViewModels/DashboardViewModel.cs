using System.Windows.Input;

namespace Fatihdgn.Todo.App.ViewModels;

public class DashboardViewModel : BindableObject
{
    private bool isMenuOpened;

    public DashboardViewModel()
    {
        ToggleMenuCommand = new Command(ToggleMenu);
    }

    public bool IsMenuOpened
    {
        get => isMenuOpened;
        set { isMenuOpened = value; OnPropertyChanged(); }
    }

    public ICommand ToggleMenuCommand { get; private set; }
    public void ToggleMenu()
    {
        IsMenuOpened = !IsMenuOpened;
    }
}
