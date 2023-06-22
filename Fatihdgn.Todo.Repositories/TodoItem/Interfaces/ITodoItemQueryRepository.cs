using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;

public interface ITodoItemQueryRepository : IQueryRepository<TodoItemEntity> { }
