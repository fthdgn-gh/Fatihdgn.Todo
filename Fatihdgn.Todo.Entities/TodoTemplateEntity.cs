using Fatihdgn.Todo.Entities.Abstractions;
using System.Text.Json.Serialization;

namespace Fatihdgn.Todo.Entities;

public class TodoTemplateEntity : IEntity<Guid>, IOwned
{
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;

    public virtual JsonEntity<List<string>> Contents { get; set; } = new();

    [JsonIgnore]
    public virtual DateTimeOffset? RemovedAt { get; set; }

    public virtual TodoUserEntity? By { get; set; }

}