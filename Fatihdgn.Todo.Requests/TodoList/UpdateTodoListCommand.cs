using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct UpdateTodoListCommand(string ById, Guid Id, TodoListUpdateDTO Model) : IRequest<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>;

