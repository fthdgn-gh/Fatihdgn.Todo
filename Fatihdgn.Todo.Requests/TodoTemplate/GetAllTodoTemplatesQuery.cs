using Fatihdgn.Todo.DTOs;
using MediatR;

namespace Fatihdgn.Todo.Requests;

// Queries
public record struct GetAllTodoTemplatesQuery(string ById) : IRequest<IEnumerable<TodoTemplateDTO>>;

