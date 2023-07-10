using Fatihdgn.Todo.API.Client;
using System.Collections.ObjectModel;

namespace Fatihdgn.Todo.App.State.Models;

public class TodoTemplateObject : BindableObject
{
    private Guid id;
    private string name;
    private ObservableCollection<string> contents = new ObservableCollection<string>();

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