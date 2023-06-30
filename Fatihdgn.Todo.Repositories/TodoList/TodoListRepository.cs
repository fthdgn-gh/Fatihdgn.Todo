using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoListRepository : TodoDBRepository<TodoListEntity, Guid>, ITodoListRepository
{
    public TodoListRepository(ITodoListCommandRepository command, ITodoListQueryRepository query) : base(command, query) { }
}