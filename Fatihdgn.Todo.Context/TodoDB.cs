using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Context;

public class TodoDB : IdentityDbContext<TodoUserEntity>
{
    public TodoDB(DbContextOptions options) : base(options) { }

    public DbSet<TodoItemEntity> Items { get; set; }
    public DbSet<TodoListEntity> Lists { get; set; }
    public DbSet<TodoTemplateEntity> Templates { get; set; }

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

    private void MarkDeletedEntities()
    {
        MarkDeletedEntities<TodoItemEntity, Guid>();
        MarkDeletedEntities<TodoListEntity, Guid>();
        MarkDeletedEntities<TodoTemplateEntity, Guid>();
    }

    public override int SaveChanges()
    {
        MarkDeletedEntities();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        MarkDeletedEntities();

        return base.SaveChangesAsync(cancellationToken);
    }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddEntityQueryFilter<TodoItemEntity, Guid>(modelBuilder);
        AddEntityQueryFilter<TodoListEntity, Guid>(modelBuilder);
        AddEntityQueryFilter<TodoTemplateEntity, Guid>(modelBuilder);
        modelBuilder.Entity<TodoTemplateEntity>().Property(x => x.Content).HasColumnType("json");
        base.OnModelCreating(modelBuilder);
    }
}