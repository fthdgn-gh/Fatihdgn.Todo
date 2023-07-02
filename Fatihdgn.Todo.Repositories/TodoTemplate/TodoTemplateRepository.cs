using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoTemplateRepository : TodoDBRepository<TodoTemplateEntity, Guid>, ITodoTemplateRepository
{
    public TodoTemplateRepository(ITodoTemplateCommandRepository command, ITodoTemplateQueryRepository query) : base(command, query) { }
}