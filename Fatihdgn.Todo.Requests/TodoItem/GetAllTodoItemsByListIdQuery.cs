using Fatihdgn.Todo.DTOs;
using MediatR;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoItemsByListIdQuery(string ById, Guid ListId) : IRequest<IEnumerable<TodoItemDTO>>;
