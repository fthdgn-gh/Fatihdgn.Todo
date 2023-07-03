using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;
using Fatihdgn.Todo.Entities.Extensions;

namespace Fatihdgn.Todo.Handlers;

public class PatchTodoTemplateCommandHandler : IRequestHandler<PatchTodoTemplateCommand, OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoTemplateRepository _repo;

    public PatchTodoTemplateCommandHandler(ITodoTemplateRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>> Handle(PatchTodoTemplateCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.Model)));

        var entity = await _repo.AsQueryable().ByUserId(request.ById).ByIdAsync(request.Id);
        if (entity == null) return new NotFound();

        request.Model.ApplyTo(entity);

        var updateResult = await _repo.UpdateAsync(entity);
        if (updateResult.IsT1) return updateResult.AsT1;
        entity = updateResult.AsT0;

        return entity.ToDTO();
    }
}