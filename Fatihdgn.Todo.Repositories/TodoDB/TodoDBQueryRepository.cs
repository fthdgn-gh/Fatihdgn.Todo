using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities.Abstractions;
using Fatihdgn.Todo.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories;

public class TodoDBQueryRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly TodoDB _context;

    public TodoDBQueryRepository(TodoDB context)
    {
        _context = context;
    }

    private DbSet<TEntity> Set => _context.Set<TEntity>();
    public async Task<OneOf<TEntity, NotFound>> ById(TKey id)
    {
        var entity = await Set.FindAsync(id);
        if (entity == null) return new NotFound();
        return entity;
    }

    public IQueryable<TEntity> AsQueryable() => Set.AsQueryable();
}
