using Fatihdgn.Todo.Entities.Abstractions;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories.Abstractions;

public interface IQueryRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<OneOf<TEntity, NotFound>> FindAsync(TKey id);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> exp);
}
