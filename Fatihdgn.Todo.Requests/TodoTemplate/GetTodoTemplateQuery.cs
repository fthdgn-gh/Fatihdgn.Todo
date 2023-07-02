using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct GetTodoTemplateQuery(string ById, Guid Id) : IRequest<OneOf<TodoTemplateDTO, NotFound>>;

