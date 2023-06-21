namespace Fatihdgn.Todo.Entities;

public interface IEntity
{
    Guid Id { get; set; }
    DateTimeOffset? RemovedAt { get; set; }

}
