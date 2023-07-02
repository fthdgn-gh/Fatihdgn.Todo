using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoTemplatesQuery(string ById) : IRequest<IEnumerable<TodoTemplateDTO>>;
public record struct GetTodoTemplateQuery(string ById, Guid Id) : IRequest<OneOf<TodoTemplateDTO, NotFound>>;

// Commands
public record struct CreateTodoTemplateCommand(string ById, TodoTemplateCreateDTO Model) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;
public record struct CreateTodoTemplateByListCommand(string ById, Guid ListId) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;
public record struct PatchTodoLTemplateommand(string ById, Guid Id, TodoTemplatePatchDTO Model) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;
public record struct UpdateTodoTemplateCommand(string ById, Guid Id, TodoTemplateUpdateDTO Model) : IRequest<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>;
public record struct RemoveTodoTemplateCommand(string ById, Guid Id) : IRequest<OneOf<None, NotFound>>;

