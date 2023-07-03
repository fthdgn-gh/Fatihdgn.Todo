using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class GetTodoTemplateQueryHandler : IRequestHandler<GetTodoTemplateQuery, OneOf<TodoTemplateDTO, NotFound>>
{
    private readonly ITodoTemplateRepository _repo;

    public GetTodoTemplateQueryHandler(ITodoTemplateRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<TodoTemplateDTO, NotFound>> Handle(GetTodoTemplateQuery request, CancellationToken cancellationToken)
    {
        var response = await _repo.AsQueryable().ByUserId(request.ById).ByIdAsync(request.Id);
        if (response == null) return new NotFound();
        return response.ToDTO();
    }
}