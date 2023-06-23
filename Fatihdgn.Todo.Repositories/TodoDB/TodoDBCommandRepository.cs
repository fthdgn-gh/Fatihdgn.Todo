using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities.Abstractions;
using Fatihdgn.Todo.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Repositories;

public class TodoDBCommandRepository<TEntity, TKey> : ICommandRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
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

    public async Task<OneOf<None, NotFound>> RemoveAsync(TKey id)
    {
        if (id == null) return new NotFound();
        var count = await Set.CountAsync(x => id.Equals(x.Id));
        if (count == 0) return new NotFound();
        var entity = new TEntity() { Id = id };
        Set.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0 ? new None() : new NotFound();
    }
}