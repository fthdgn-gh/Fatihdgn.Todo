using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct PatchTodoTemplateCommand(string ById, Guid Id, TodoTemplatePatchDTO Model) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;

