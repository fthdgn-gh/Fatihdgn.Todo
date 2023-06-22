using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Abstractions;
using Fatihdgn.Todo.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories;

public class TodoDBRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly ICommandRepository<TEntity> _command;
    private readonly IQueryRepository<TEntity> _query;

    public TodoDBRepository(ICommandRepository<TEntity> command, IQueryRepository<TEntity> query)
    {
        _command = command;
        _query = query;
    }

    public IQueryRepository<TEntity> Query => _query;
    public ICommandRepository<TEntity> Command => _command;


    public async Task<OneOf<TEntity, NotFound>> FindAsync(Guid id) => await _query.FindAsync(id);

    public IQueryable<TEntity> GetAll() => _query.GetAll();
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> exp) => _query.Where(exp);

    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> AddAsync(TEntity entity) => await _command.AddAsync(entity);
    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> RemoveAsync(TEntity entity) => await _command.RemoveAsync(entity);
    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> UpdateAsync(TEntity entity) => await _command.UpdateAsync(entity);
}