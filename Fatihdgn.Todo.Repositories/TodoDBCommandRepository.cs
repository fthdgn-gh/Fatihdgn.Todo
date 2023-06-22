using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities.Abstractions;
using Fatihdgn.Todo.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Repositories;

public class TodoDBCommandRepository<TEntity> : ICommandRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly TodoDB _context;

    public TodoDBCommandRepository(TodoDB context)
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
        if (entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }
        var entry = Set.Update(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }
}