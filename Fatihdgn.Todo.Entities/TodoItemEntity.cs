namespace Fatihdgn.Todo.Entities;

public class TodoItemEntity
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTimeOffset? RemindAt { get; set; }
    public DateTimeOffset? DueAt { get; set; }

}