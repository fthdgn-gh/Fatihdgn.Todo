using Fatihdgn.Todo.Entities.Abstractions;
using System.Text.Json.Serialization;

namespace Fatihdgn.Todo.Entities;

public class TodoItemEntity : IEntity<Guid>, IOwned
{
    public virtual Guid Id { get; set; }
    public virtual string Content { get; set; } = string.Empty;
    public virtual string Note { get; set; } = string.Empty;
    public virtual bool IsCompleted { get; set; }

    [JsonIgnore]
    public virtual DateTimeOffset? RemovedAt { get; set; }

    public virtual TodoListEntity? List { get; set; }
    public virtual TodoUserEntity? By { get; set; }

}