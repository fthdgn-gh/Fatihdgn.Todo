using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemRepository : TodoDBRepository<TodoItemEntity>, ITodoItemRepository
{
    public TodoItemRepository(ITodoItemCommandRepository command, ITodoItemQueryRepository query) : base(command, query) { }
}