using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoUserCommandRepository : TodoDBCommandRepository<TodoUserEntity, string>, ITodoUserCommandRepository
{
    public TodoUserCommandRepository(TodoDB context) : base(context) { }
}