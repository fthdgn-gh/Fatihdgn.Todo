using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;
public record struct RemoveTodoItemCommand(string ById, Guid Id) : IRequest<OneOf<None, NotFound>>;