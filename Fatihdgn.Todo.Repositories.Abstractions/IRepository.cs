using Fatihdgn.Todo.Entities.Abstractions;

namespace Fatihdgn.Todo.Repositories.Abstractions;

public interface IRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>, ICommandRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    IQueryRepository<TEntity, TKey> Query { get; }
    ICommandRepository<TEntity, TKey> Command { get; }
}