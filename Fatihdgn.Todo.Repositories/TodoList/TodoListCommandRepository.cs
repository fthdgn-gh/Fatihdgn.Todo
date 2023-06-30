using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoListCommandRepository : TodoDBCommandRepository<TodoListEntity, Guid>, ITodoListCommandRepository
{
    public TodoListCommandRepository(TodoDB context) : base(context) { }
}