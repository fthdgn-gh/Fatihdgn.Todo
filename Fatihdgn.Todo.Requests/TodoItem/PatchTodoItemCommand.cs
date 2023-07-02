using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct PatchTodoItemCommand(string ById, Guid Id, TodoItemPatchDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
