using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Context;

public class TodoDB : IdentityDbContext<TodoUserEntity>
{
    public TodoDB(DbContextOptions options) : base(options) { }

    public DbSet<TodoItemEntity> Items { get; set; }

    private void MarkDeletedEntities<TEntity, TKey>()
        where TEntity : class, IEntity<TKey>
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

    private void AddEntityQueryFilter<TEntity, TKey>(ModelBuilder modelBuilder)
        where TEntity : class, IEntity<TKey>
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(entity => entity.RemovedAt == null);
    }

    public override int SaveChanges()
    {
        MarkDeletedEntities<TodoItemEntity, Guid>();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        MarkDeletedEntities<TodoItemEntity, Guid>();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddEntityQueryFilter<TodoItemEntity, Guid>(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}