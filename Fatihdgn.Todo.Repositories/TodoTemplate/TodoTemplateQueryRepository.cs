using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoTemplateQueryRepository : TodoDBQueryRepository<TodoTemplateEntity, Guid>, ITodoTemplateQueryRepository
{
    public TodoTemplateQueryRepository(TodoDB context) : base(context) { }
}