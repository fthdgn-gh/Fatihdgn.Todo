using Fatihdgn.Todo.Entities.Abstractions;

namespace Fatihdgn.Todo.Repositories.Abstractions;

public interface IRepository<TEntity> : IQueryRepository<TEntity>, ICommandRepository<TEntity>
    where TEntity : class, IEntity 
{

}