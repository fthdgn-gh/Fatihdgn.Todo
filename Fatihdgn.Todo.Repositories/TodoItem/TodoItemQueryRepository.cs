using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemQueryRepository : TodoDBQueryRepository<TodoItemEntity, Guid>, ITodoItemQueryRepository
{
    public TodoItemQueryRepository(TodoDB context) : base(context) { }
}