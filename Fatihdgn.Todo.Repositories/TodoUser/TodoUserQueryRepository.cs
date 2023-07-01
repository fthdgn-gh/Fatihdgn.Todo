using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoUserQueryRepository : TodoDBQueryRepository<TodoUserEntity, string>, ITodoUserQueryRepository
{
    public TodoUserQueryRepository(TodoDB context) : base(context) { }
}