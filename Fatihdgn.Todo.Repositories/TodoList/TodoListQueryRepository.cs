using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoListQueryRepository : TodoDBQueryRepository<TodoListEntity, Guid>, ITodoListQueryRepository
{
    public TodoListQueryRepository(TodoDB context) : base(context) { }
}