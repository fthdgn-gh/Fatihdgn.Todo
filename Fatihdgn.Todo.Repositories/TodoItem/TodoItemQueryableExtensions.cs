using Fatihdgn.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.Repositories;

public static class TodoItemQueryableExtensions
{
#pragma warning disable CS8602 // We are creating a query based on the expression. This code will not run in the runtime.
    public static IQueryable<TodoItemEntity> ByUserId(this IQueryable<TodoItemEntity> self, string id) => self.Where(item => item.By.Id.Equals(id));
    public static IQueryable<TodoItemEntity> ByListId(this IQueryable<TodoItemEntity> self, Guid id) => self.Where(item => item.List.Id.Equals(id));
#pragma warning restore CS8602
}
