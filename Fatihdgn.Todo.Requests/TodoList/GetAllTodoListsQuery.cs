using Fatihdgn.Todo.DTOs;
using MediatR;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoListsQuery(string ById) : IRequest<IEnumerable<TodoListDTO>>;

