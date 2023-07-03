using Fatihdgn.Todo.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.Entities.Extensions;

public static class IOwnedEntityExtensions
{
#pragma warning disable CS8602 // We are creating a query based on the expression. This code will not run in the runtime.
    
    public static IQueryable<TEntity> ByUserId<TEntity, TKey>(this IQueryable<TEntity> self, string id)
        where TEntity : class, IEntity<TKey>, IOwned
        => self.Where(item => item.By.Id.Equals(id));

    public static IQueryable<TEntity> ByUserId<TEntity>(this IQueryable<TEntity> self, string id)
        where TEntity : class, IEntity<Guid>, IOwned
        => self.Where(item => item.By.Id.Equals(id));

#pragma warning restore CS8602
}
