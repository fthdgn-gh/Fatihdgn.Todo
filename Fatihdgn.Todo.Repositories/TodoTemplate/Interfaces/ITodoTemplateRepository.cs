using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;
public interface ITodoTemplateRepository : IRepository<TodoTemplateEntity, Guid> { }
