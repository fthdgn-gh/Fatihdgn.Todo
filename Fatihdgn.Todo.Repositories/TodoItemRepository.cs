using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoItemRepository : TodoDBRepository<TodoItemEntity>
{
    public TodoItemRepository(TodoDB context) : base(context) { }
}