using Fatihdgn.Todo.Entities.Abstractions;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Repositories.Abstractions;

public interface ICommandRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<OneOf<TEntity, Error<ArgumentNullException>>> AddAsync(TEntity entity);
    Task<OneOf<TEntity, Error<ArgumentNullException>>> UpdateAsync(TEntity entity);
    Task<OneOf<TEntity, Error<ArgumentNullException>>> RemoveAsync(TEntity entity);
    Task<OneOf<None, NotFound>> RemoveAsync(TKey id);
}
