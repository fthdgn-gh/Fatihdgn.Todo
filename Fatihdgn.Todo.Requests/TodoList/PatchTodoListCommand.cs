using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct PatchTodoListCommand(string ById, Guid Id, TodoListPatchDTO Model) : IRequest<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>;

