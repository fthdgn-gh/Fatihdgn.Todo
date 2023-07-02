using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct UpdateTodoItemCommand(string ById, Guid Id, TodoItemUpdateDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
