using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct UpdateTodoTemplateCommand(string ById, Guid Id, TodoTemplateUpdateDTO Model) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;

