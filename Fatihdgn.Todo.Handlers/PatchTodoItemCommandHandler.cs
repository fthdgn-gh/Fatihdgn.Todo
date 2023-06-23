using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class PatchTodoItemCommandHandler : IRequestHandler<PatchTodoItemCommand, OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoItemRepository _repo;

    public PatchTodoItemCommandHandler(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>> Handle(PatchTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.Model)));

        var result = await _repo.FindAsync(request.Id);
        if (result.IsT1) return result.AsT1;

        var entity = result.AsT0;
        request.Model.ApplyTo(entity);

        var updateResult = await _repo.UpdateAsync(entity);
        if (updateResult.IsT1) return result.AsT1;
        entity = result.AsT0;

        return entity.ToDTO();
    }
}