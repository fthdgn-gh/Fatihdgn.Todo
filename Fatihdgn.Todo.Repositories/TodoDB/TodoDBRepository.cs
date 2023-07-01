using Fatihdgn.Todo.Entities.Abstractions;
using Fatihdgn.Todo.Repositories.Abstractions;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories;

public class TodoDBRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly ICommandRepository<TEntity, TKey> _command;
    private readonly IQueryRepository<TEntity, TKey> _query;

    public TodoDBRepository(ICommandRepository<TEntity, TKey> command, IQueryRepository<TEntity, TKey> query)
    {
        _command = command;
        _query = query;
    }

    public IQueryRepository<TEntity, TKey> Query => _query;
    public ICommandRepository<TEntity, TKey> Command => _command;


    public async Task<OneOf<TEntity, NotFound>> ByIdAsync(TKey id) => await _query.ByIdAsync(id);

    public IQueryable<TEntity> AsQueryable() => _query.AsQueryable();

    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> AddAsync(TEntity entity) => await _command.AddAsync(entity);
    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> RemoveAsync(TEntity entity) => await _command.RemoveAsync(entity);
    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> UpdateAsync(TEntity entity) => await _command.UpdateAsync(entity);
    public async Task<OneOf<None, NotFound>> RemoveAsync(TKey id) => await _command.RemoveAsync(id);
}