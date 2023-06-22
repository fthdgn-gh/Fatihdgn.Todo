namespace Fatihdgn.Todo.Entities.Abstractions;

public interface IEntity
{
    Guid Id { get; set; }
    DateTimeOffset? RemovedAt { get; set; }

}
