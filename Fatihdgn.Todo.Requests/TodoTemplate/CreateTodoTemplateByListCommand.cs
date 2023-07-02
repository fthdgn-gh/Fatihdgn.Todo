using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct CreateTodoTemplateByListCommand(string ById, Guid ListId) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;