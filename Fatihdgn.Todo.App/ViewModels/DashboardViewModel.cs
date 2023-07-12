using CommunityToolkit.Mvvm.Input;
using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.Providers;
using Fatihdgn.Todo.App.State;
using Fatihdgn.Todo.App.State.Models;
using System.Collections.ObjectModel;

namespace Fatihdgn.Todo.App.ViewModels;

public partial class DashboardViewModel : BindableObject
{
    private bool isMenuOpened;
    private readonly IFatihdgnTodoClient _client;
    private readonly AppState _state;
    public DashboardViewModel()
    {
        _client = FatihdgnTodoClientProvider.Current;
        _state = AppState.Instance;
    }

    public bool IsMenuOpened
    {
        get => isMenuOpened;
        set { isMenuOpened = value; OnPropertyChanged(); }
    }

    [RelayCommand]
    public void ToggleMenu()
    {
        IsMenuOpened = !IsMenuOpened;
    }

    public ObservableCollection<TodoListDTO> Lists => AppState.Instance.Lists;
    public ObservableCollection<TodoItemDTO> Items => AppState.Instance.Items;
    public TodoListObject CurrentTodoList => AppState.Instance.CurrentTodoList;

    [RelayCommand]
    private async Task SelectTodoListAsync(TodoListDTO list)
    {
        _state.CurrentTodoListId = list.Id;
        _state.CurrentTodoList.MapFrom(list);
        _state.Items.Clear();
        foreach (var item in await _client.GetAllItemsByListIdAsync(list.Id))
            _state.Items.Add(item);
    }

    [RelayCommand]
    private async Task TodoItemCheckedChangedAsync(object obj)
    {
        if (obj is not TodoItemDTO item) return;
        _state.CurrentTodoItemId = item.Id;
        _state.CurrentTodoItem.MapFrom(item);
        await _client.PatchItemAsync(item.Id, new TodoItemPatchDTO { IsCompleted = !item.IsCompleted });
    }
}
