using Fatihdgn.Todo.Entities.Abstractions;
using System.Text.Json.Serialization;

namespace Fatihdgn.Todo.Entities;

public class TodoItemEntity : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTimeOffset? RemindAt { get; set; }
    public DateTimeOffset? DueAt { get; set; }
    public bool IsCompleted { get; set; }

    [JsonIgnore]
    public DateTimeOffset? RemovedAt { get; set; }

    public TodoUserEntity? By { get; set; }

}