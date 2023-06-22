using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemRepository : TodoDBRepository<TodoItemEntity>
{
    public TodoItemRepository(ICommandRepository<TodoItemEntity> command, IQueryRepository<TodoItemEntity> query) : base(command, query) { }
}