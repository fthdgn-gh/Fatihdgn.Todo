using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Commands
public record struct CreateTodoTemplateCommand(string ById, TodoTemplateCreateDTO Model) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;

