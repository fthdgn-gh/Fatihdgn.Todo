using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;


namespace Fatihdgn.Todo.Handlers;

public class RemoveTodoListCommandHandler : IRequestHandler<RemoveTodoListCommand, OneOf<None, NotFound>>
{
    private readonly ITodoListRepository _repo;

    public RemoveTodoListCommandHandler(ITodoListRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<None, NotFound>> Handle(RemoveTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.AsQueryable().ByUserId(request.ById).ByIdAsync(request.Id);
        if (entity == null) return new NotFound();

        await _repo.RemoveAsync(entity);
        return new None();
    }
}