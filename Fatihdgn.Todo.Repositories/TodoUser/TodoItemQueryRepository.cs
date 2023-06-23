using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemUserRepository : TodoDBQueryRepository<TodoUserEntity, string>, ITodoUserQueryRepository
{
    public TodoItemUserRepository(TodoDB context) : base(context) { }
}