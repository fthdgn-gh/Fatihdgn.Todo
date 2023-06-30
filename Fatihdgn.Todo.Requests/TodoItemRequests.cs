using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoItemsByListIdQuery(string ById, Guid ListId) : IRequest<IEnumerable<TodoItemDTO>>;
public record struct GetTodoItemQuery(string ById, Guid Id) : IRequest<OneOf<TodoItemDTO, NotFound>>;

// Commands
public record struct CreateTodoItemCommand(string ById, TodoItemCreateDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
public record struct PatchTodoItemCommand(string ById, Guid Id, TodoItemPatchDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
public record struct UpdateTodoItemCommand(string ById, Guid Id, TodoItemUpdateDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
public record struct RemoveTodoItemCommand(string ById, Guid Id) : IRequest<OneOf<None, NotFound>>;

