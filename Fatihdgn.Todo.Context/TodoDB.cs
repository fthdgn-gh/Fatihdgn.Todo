using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Context;

public class TodoDB : DbContext
{
    public TodoDB(DbContextOptions options) : base(options) { }

    public DbSet<TodoItemEntity> Items { get; set; }

    private void MarkDeletedEntities<TEntity>()
        where TEntity : class, IEntity
    {
        foreach (var entry in ChangeTracker.Entries<TEntity>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.RemovedAt = DateTime.UtcNow;
            }
        }
    }

    private void AddEntityQueryFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class, IEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(entity => entity.RemovedAt == null);
    }

    public override int SaveChanges()
    {
        MarkDeletedEntities<TodoItemEntity>();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        MarkDeletedEntities<TodoItemEntity>();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddEntityQueryFilter<TodoItemEntity>(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}