using Fatihdgn.Todo.API.Client;

namespace Fatihdgn.Todo.App.State.Models;

public class TodoItemObject : BindableObject
{
    private Guid id;
    private string content;
    private string note;
    private bool isCompleted;

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
