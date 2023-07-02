using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct GetTodoItemQuery(string ById, Guid Id) : IRequest<OneOf<TodoItemDTO, NotFound>>;
