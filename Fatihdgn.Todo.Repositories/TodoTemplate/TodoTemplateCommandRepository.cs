using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoTemplateCommandRepository : TodoDBCommandRepository<TodoTemplateEntity, Guid>, ITodoTemplateCommandRepository
{
    public TodoTemplateCommandRepository(TodoDB context) : base(context) { }
}