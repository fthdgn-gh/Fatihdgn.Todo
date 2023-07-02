using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Commands
public record struct CreateTodoItemCommand(string ById, TodoItemCreateDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
