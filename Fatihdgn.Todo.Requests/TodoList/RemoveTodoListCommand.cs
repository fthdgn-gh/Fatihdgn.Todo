using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct RemoveTodoListCommand(string ById, Guid Id) : IRequest<OneOf<None, NotFound>>;

