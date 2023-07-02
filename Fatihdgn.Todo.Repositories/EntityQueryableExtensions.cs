using Fatihdgn.Todo.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Repositories;

public static class EntityQueryableExtensions
{
    public static async Task<TEntity?> ByIdAsync<TEntity, TKey>(this IQueryable<TEntity> self, TKey id) where TEntity : class, IEntity<TKey>
    {
        return await self.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public static TEntity? ById<TEntity, TKey>(this IQueryable<TEntity> self, TKey id) where TEntity : class, IEntity<TKey>
    {
        return self.FirstOrDefault(x => x.Id.Equals(id));
    }
}
