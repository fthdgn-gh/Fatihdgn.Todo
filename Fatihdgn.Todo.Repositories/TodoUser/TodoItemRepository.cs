using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Repositories;

public class TodoUserRepository : TodoDBRepository<TodoUserEntity, string>, ITodoUserRepository
{
    public TodoUserRepository(ITodoUserCommandRepository command, ITodoUserQueryRepository query) : base(command, query) { }
}