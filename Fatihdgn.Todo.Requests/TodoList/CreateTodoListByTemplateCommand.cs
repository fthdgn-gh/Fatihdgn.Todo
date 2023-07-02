using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct CreateTodoListByTemplateCommand(string ById, Guid TemplateId) : IRequest<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>;

