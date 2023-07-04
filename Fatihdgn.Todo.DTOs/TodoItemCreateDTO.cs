namespace Fatihdgn.Todo.DTOs;

public class TodoItemCreateDTO
{
    public string Content { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public Guid ListId { get; set; }
}
