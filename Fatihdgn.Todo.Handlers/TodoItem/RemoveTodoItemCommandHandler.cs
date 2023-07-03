using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;
using Fatihdgn.Todo.Entities.Extensions;


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
        var entity = await _repo.AsQueryable().ByUserId(request.ById).ByIdAsync(request.Id);
        if (entity == null) return new NotFound();

        await _repo.RemoveAsync(entity);
        return new None();
    }
}