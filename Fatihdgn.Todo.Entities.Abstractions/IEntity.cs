namespace Fatihdgn.Todo.Entities.Abstractions;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
    DateTimeOffset? RemovedAt { get; set; }
}
