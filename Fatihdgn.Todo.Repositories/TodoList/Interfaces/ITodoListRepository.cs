using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;
public interface ITodoListRepository : IRepository<TodoListEntity, Guid> { }
