using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;

public interface ITodoListQueryRepository : IQueryRepository<TodoListEntity, Guid> { }
