using Fatihdgn.Todo.Entities.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fatihdgn.Todo.Entities;

public class TodoTemplateEntity : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public JsonDocument Content { get; set; }

    [JsonIgnore]
    public DateTimeOffset? RemovedAt { get; set; }

    public TodoUserEntity? By { get; set; }

}