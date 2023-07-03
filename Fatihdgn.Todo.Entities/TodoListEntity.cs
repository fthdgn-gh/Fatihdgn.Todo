using Fatihdgn.Todo.Entities.Abstractions;
using System.Text.Json.Serialization;

namespace Fatihdgn.Todo.Entities;

public class TodoListEntity : IEntity<Guid>, IOwned
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public DateTimeOffset? RemovedAt { get; set; }

    public TodoUserEntity? By { get; set; }

    public ICollection<TodoItemEntity> Items { get; set; } = new List<TodoItemEntity>();
}