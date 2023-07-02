using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Commands
public record struct CreateTodoListCommand(string ById, TodoListCreateDTO Model) : IRequest<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>;

