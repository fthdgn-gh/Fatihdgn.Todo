namespace Fatihdgn.Todo.Models;

public struct TodoItem
{
    public string Content { get; set; }
    public string Note { get; set; }
    public DateTimeOffset? RemindAt { get; set; }
    public DateTimeOffset? DueAt { get; set; }
}