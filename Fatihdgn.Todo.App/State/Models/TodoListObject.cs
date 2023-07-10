using Fatihdgn.Todo.API.Client;

namespace Fatihdgn.Todo.App.State.Models;

public class TodoListObject : BindableObject
{
    private Guid id;
    private string name;

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
