using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;

namespace Fatihdgn.Todo.Repositories;
public interface ITodoUserRepository : IRepository<TodoUserEntity, string> { }
