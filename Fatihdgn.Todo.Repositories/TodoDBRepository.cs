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
    private readonly TodoDB _context;

    public TodoDBRepository(TodoDB context)
    {
        _context = context;
    }

    private DbSet<TEntity> Set => _context.Set<TEntity>();

    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }

        var entry = await Set.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<OneOf<TEntity, NotFound>> FindAsync(Guid id)
    {
        var entity = await Set.FindAsync(id);
        if (entity == null) return new NotFound();
        return entity;
    }

    public IQueryable<TEntity> GetAll() => Set.AsQueryable();

    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> RemoveAsync(TEntity entity)
    {
        if (entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }
        var entry = Set.Remove(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<OneOf<TEntity, Error<ArgumentNullException>>> UpdateAsync(TEntity entity)
    {
        if(entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }
        var entry = Set.Update(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> exp) => Set.Where(exp);
}