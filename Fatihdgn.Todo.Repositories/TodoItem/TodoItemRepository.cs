using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemRepository : TodoDBRepository<TodoItemEntity, Guid>, ITodoItemRepository
{
    public TodoItemRepository(ITodoItemCommandRepository command, ITodoItemQueryRepository query) : base(command, query) { }
}