using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public static class TodoListQueryableExtensions
{
#pragma warning disable CS8602 // We are creating a query based on the expression. This code will not run in the runtime.
    public static IQueryable<TodoListEntity> ByUserId(this IQueryable<TodoListEntity> self, string id) => self.Where(item => item.By.Id.Equals(id));
#pragma warning restore CS8602
}
