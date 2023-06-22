using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities.Abstractions;

namespace Fatihdgn.Todo.Handlers;

public class RemoveTodoItemCommandHandler : IRequestHandler<RemoveTodoItemCommand, OneOf<None, NotFound>>
{
    private readonly ITodoItemRepository _repo;

    public RemoveTodoItemCommandHandler(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<None, NotFound>> Handle(RemoveTodoItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _repo.RemoveAsync(request.Id);
        return result;
    }
}