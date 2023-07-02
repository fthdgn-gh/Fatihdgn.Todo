using Fatihdgn.Todo.Entities.Abstractions;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Repositories.Abstractions;

public interface IQueryRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<OneOf<TEntity, NotFound>> ByIdAsync(TKey id);
    IQueryable<TEntity> AsQueryable();
}
