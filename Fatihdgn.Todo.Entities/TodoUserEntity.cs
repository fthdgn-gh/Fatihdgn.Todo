using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Fatihdgn.Todo.Entities;

public class TodoUserEntity : IdentityUser, IEntity<string>
{
    public ICollection<TodoListEntity> Lists { get; set; } = new List<TodoListEntity>();
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RemovedAt { get; set; }
}
