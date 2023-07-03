using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Fatihdgn.Todo.Entities;

public class TodoUserEntity : IdentityUser, IEntity<string>
{
    public virtual ICollection<TodoListEntity> Lists { get; set; } = new List<TodoListEntity>();
    public virtual string? RefreshToken { get; set; }
    public virtual DateTimeOffset? RemovedAt { get; set; }
}
