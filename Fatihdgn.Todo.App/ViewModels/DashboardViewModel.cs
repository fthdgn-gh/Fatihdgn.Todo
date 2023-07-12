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
    public ObservableCollection<TodoTemplateDTO> Templates => AppState.Instance.Templates;
    public TodoListObject CurrentTodoList => AppState.Instance.CurrentTodoList;

    [RelayCommand]
    public async Task SelectTodoListAsync(TodoListDTO list)
    {
        _state.CurrentTodoListId = list.Id;
        _state.CurrentTodoList.MapFrom(list);
        _state.Items.Clear();
        foreach (var item in await _client.GetAllItemsByListIdAsync(list.Id))
            _state.Items.Add(item);
    }


    public async Task TodoItemIsCompletedChangedAsync(TodoItemDTO item, bool isCompleted)
    {
        if (item is null) return;
        _state.CurrentTodoItemId = item.Id;
        _state.CurrentTodoItem.MapFrom(item);
        _state.CurrentTodoItem.IsCompleted = isCompleted;
        await _client.PatchItemAsync(item.Id, new TodoItemPatchDTO { IsCompleted = isCompleted });
    }
}
