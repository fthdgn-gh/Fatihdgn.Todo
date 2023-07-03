using Fatihdgn.Todo.Entities.Abstractions;
using System.Text.Json.Serialization;

namespace Fatihdgn.Todo.Entities;

public class TodoListEntity : IEntity<Guid>, IOwned
{
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual DateTimeOffset? RemovedAt { get; set; }

    public virtual TodoUserEntity? By { get; set; }

    public virtual ICollection<TodoItemEntity> Items { get; set; } = new List<TodoItemEntity>();
}