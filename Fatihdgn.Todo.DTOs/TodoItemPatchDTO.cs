namespace Fatihdgn.Todo.DTOs;

public class TodoItemPatchDTO
{
    public string? Content { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset? RemindAt { get; set; }
    public DateTimeOffset? DueAt { get; set; }
}