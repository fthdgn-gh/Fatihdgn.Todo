using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoListsQuery(string ById) : IRequest<IEnumerable<TodoListDTO>>;
public record struct GetTodoListQuery(string ById, Guid Id) : IRequest<OneOf<TodoListDTO, NotFound>>;

// Commands
public record struct CreateTodoListCommand(string ById, TodoListCreateDTO Model) : IRequest<OneOf<TodoListDTO, Error<ArgumentNullException>>>;
public record struct PatchTodoListCommand(string ById, Guid Id, TodoListPatchDTO Model) : IRequest<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>;
public record struct UpdateTodoListCommand(string ById, Guid Id, TodoListUpdateDTO Model) : IRequest<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>;
public record struct RemoveTodoListCommand(string ById, Guid Id) : IRequest<OneOf<None, NotFound>>;

