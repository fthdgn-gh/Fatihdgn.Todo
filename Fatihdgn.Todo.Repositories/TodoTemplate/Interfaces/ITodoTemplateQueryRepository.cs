using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;

public interface ITodoTemplateQueryRepository : IQueryRepository<TodoTemplateEntity, Guid> { }
