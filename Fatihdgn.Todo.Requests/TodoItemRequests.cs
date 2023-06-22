using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoItemsQuery() : IRequest<IEnumerable<TodoItemDTO>>;
public record struct GetTodoItemQuery(Guid Id) : IRequest<OneOf<TodoItemDTO, NotFound>>;

// Commands
public record struct CreateTodoItemCommand(TodoItemCreateDTO Model) : IRequest<OneOf<TodoItemDTO, Error<ArgumentNullException>>>;
public record struct PatchTodoItemCommand(Guid Id, TodoItemPatchDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
public record struct UpdateTodoItemCommand(Guid Id, TodoItemUpdateDTO Model) : IRequest<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>;
public record struct RemoveTodoItemCommand(Guid Id) : IRequest<OneOf<None, NotFound>>;

