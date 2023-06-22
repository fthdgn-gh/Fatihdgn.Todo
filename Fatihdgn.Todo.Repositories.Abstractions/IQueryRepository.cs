using Fatihdgn.Todo.Entities.Abstractions;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories.Abstractions;

public interface IQueryRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<OneOf<TEntity, NotFound>> FindAsync(Guid id);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> exp);
}
