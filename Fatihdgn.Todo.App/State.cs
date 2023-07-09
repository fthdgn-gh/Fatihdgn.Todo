using Fatihdgn.Todo.API.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fatihdgn.Todo.App;

public class State : BindableObject
{
    private static Lazy<State> instance = new Lazy<State>();
    public static State Instance => instance.Value;

    private StateUser user;
    private Guid currentTodoListId;
    private TodoListObject currentTodoList;
    private Guid currentTodoItemId;
    private TodoItemObject currentTodoItem;
    private Guid currentTodoTemplateId;
    private TodoTemplateObject currentTodoTemplate;
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

public class StateUser : BindableObject
{
    private string email;
    private string accessToken;
    private string refreshToken;

    public string Email
    {
        get => email;
        set
        {
            email = value;
            OnPropertyChanged();
        }
    }

    public string AccessToken
    {
        get => accessToken;
        set
        {
            accessToken = value;
            OnPropertyChanged();
        }
    }

    public string RefreshToken
    {
        get => refreshToken;
        set
        {
            refreshToken = value;
            OnPropertyChanged();
        }
    }
}

public class TodoListObject : BindableObject
{
    private Guid id;
    private string name;

    public TodoListObject(TodoListDTO values)
    {
        MapFrom(values);
    }

    public Guid Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(); }
    }


    public string Name
    {
        get { return name; }
        set { name = value; OnPropertyChanged(); }
    }

    public TodoListDTO MapTo(TodoListDTO values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        values.Id = Id;
        values.Name = Name;
        return values;
    }

    public TodoListDTO MapFrom(TodoListDTO values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        Id = values.Id;
        Name = values.Name;
        return values;
    }
}

public class TodoItemObject : BindableObject
{
    private Guid id;
    private string content;
    private string note;
    private bool isCompleted;

    public TodoItemObject(TodoItemDTO values)
    {
        MapFrom(values);
    }

    public Guid Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(); }
    }

    public string Content
    {
        get { return content; }
        set { content = value; OnPropertyChanged(); }
    }

    public string Note
    {
        get { return note; }
        set { note = value; OnPropertyChanged(); }
    }

    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; OnPropertyChanged(); }
    }

    public TodoItemDTO MapFrom(TodoItemDTO values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        Id = values.Id;
        Content = values.Content;
        Note = values.Note;
        IsCompleted = values.IsCompleted;
        return values;
    }

    public TodoItemDTO MapTo(TodoItemDTO values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        values.Id = Id;
        values.Content = Content;
        values.Note = Note;
        values.IsCompleted = IsCompleted;
        return values;
    }
}

public class TodoTemplateObject : BindableObject
{
    private Guid id;
    private string name;
    private ObservableCollection<string> contents = new ObservableCollection<string>();


    public TodoTemplateObject(TodoTemplateDTO values)
    {
        MapFrom(values);
    }

    public Guid Id
    {
        get => id;
        set { id = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(); }
    }

    ObservableCollection<string> Contents
    {
        get => contents;
        set { contents = value; OnPropertyChanged(); }
    }

    public TodoTemplateDTO MapFrom(TodoTemplateDTO values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        Id = values.Id;
        Name = values.Name;
        Contents = new ObservableCollection<string>(values.Contents);
        return values;
    }

    public TodoTemplateDTO MapTo(TodoTemplateDTO values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        values.Id = Id;
        values.Name = Name;
        values.Contents = new List<string>(Contents);
        return values;
    }
}