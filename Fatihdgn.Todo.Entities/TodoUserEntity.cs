using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Fatihdgn.Todo.Entities;

public class TodoUserEntity : IdentityUser, IEntity<string>
{
    public ICollection<TodoUserEntity> Todos { get; set; } = new List<TodoUserEntity>();
    public string RefreshToken { get; set; }
    public DateTimeOffset? RemovedAt { get; set; }
}
