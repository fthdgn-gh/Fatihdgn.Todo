namespace Fatihdgn.Todo.DTOs;

public class TodoItemCreateDTO
{
    public string Content { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTimeOffset? RemindAt { get; set; }
    public DateTimeOffset? DueAt { get; set; }
    public bool IsCompleted { get; set; }
}
