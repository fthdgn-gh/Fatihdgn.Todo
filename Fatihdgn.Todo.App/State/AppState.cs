using Fatihdgn.Todo.API.Client;
using Fatihdgn.Todo.App.State.Models;
using System.Collections.ObjectModel;

namespace Fatihdgn.Todo.App.State;

public class AppState : BindableObject
{
    private static Lazy<AppState> instance = new Lazy<AppState>();
    public static AppState Instance => instance.Value;

    private StateUser user = new StateUser();
    private Guid currentTodoListId;
    private TodoListObject currentTodoList = new TodoListObject();
    private Guid currentTodoItemId;
    private TodoItemObject currentTodoItem = new TodoItemObject();
    private Guid currentTodoTemplateId;
    private TodoTemplateObject currentTodoTemplate = new TodoTemplateObject();
    private ObservableCollection<TodoListDTO> lists = new ObservableCollection<TodoListDTO>();
    private ObservableCollection<TodoItemDTO> items = new ObservableCollection<TodoItemDTO>();
    private ObservableCollection<TodoTemplateDTO> templates = new ObservableCollection<TodoTemplateDTO>();

    public StateUser User
    {
        get => user;
        set
        {
            user = value;
            OnPropertyChanged();
        }
    }

    public Guid CurrentTodoListId
    {
        get => currentTodoListId;
        set
        {
            currentTodoListId = value;
            OnPropertyChanged();
        }
    }

    public TodoListObject CurrentTodoList
    {
        get => currentTodoList;
        set
        {
            currentTodoList = value;
            OnPropertyChanged();
        }
    }

    public Guid CurrentTodoItemId
    {
        get => currentTodoItemId;
        set
        {
            currentTodoItemId = value;
            OnPropertyChanged();
        }
    }

    public TodoItemObject CurrentTodoItem
    {
        get => currentTodoItem;
        set
        {
            currentTodoItem = value;
            OnPropertyChanged();
        }
    }

    public Guid CurrentTodoTemplateId
    {
        get => currentTodoTemplateId;
        set
        {
            currentTodoTemplateId = value;
            OnPropertyChanged();
        }
    }

    public TodoTemplateObject CurrentTodoTemplate
    {
        get => currentTodoTemplate;
        set
        {
            currentTodoTemplate = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<TodoListDTO> Lists
    {
        get => lists;
        set
        {
            lists = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<TodoItemDTO> Items
    {
        get => items;
        set
        {
            items = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<TodoTemplateDTO> Templates
    {
        get => templates;
        set
        {
            templates = value;
            OnPropertyChanged();
        }
    }
}
