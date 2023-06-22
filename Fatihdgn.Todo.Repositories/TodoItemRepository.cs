using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Abstractions;
using Fatihdgn.Todo.Repositories.Abstractions;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemRepository : IRepository<TodoItemEntity>
{
    private readonly TodoDB _context;

    public TodoItemRepository(TodoDB context)
    {
        _context = context;
    }

    public async Task<OneOf<TodoItemEntity, Error<ArgumentNullException>>> AddAsync(TodoItemEntity entity)
    {
        if (entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }

        var entry = await _context.Items.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<OneOf<TodoItemEntity, NotFound>> FindAsync(Guid id)
    {
        var entity = await _context.Items.FindAsync(id);
        if (entity == null) return new NotFound();
        return entity;
    }

    public IQueryable<TodoItemEntity> GetAll() => _context.Items;

    public async Task<OneOf<TodoItemEntity, Error<ArgumentNullException>>> RemoveAsync(TodoItemEntity entity)
    {
        if (entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }
        var entry = _context.Items.Remove(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<OneOf<TodoItemEntity, Error<ArgumentNullException>>> UpdateAsync(TodoItemEntity entity)
    {
        if(entity == null)
        {
            return new Error<ArgumentNullException>(new ArgumentNullException(nameof(entity)));
        }
        var entry = _context.Items.Update(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public IQueryable<TodoItemEntity> Where(Expression<Func<TodoItemEntity, bool>> exp) => _context.Items.Where(exp);
}