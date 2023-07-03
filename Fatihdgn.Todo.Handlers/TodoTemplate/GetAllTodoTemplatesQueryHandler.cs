using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.Handlers;

public class GetAllTodoTemplatesQueryHandler : IRequestHandler<GetAllTodoTemplatesQuery, IEnumerable<TodoTemplateDTO>>
{
    private readonly ITodoTemplateRepository _repo;

    public GetAllTodoTemplatesQueryHandler(ITodoTemplateRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<TodoTemplateDTO>> Handle(GetAllTodoTemplatesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repo.AsQueryable().ByUserId<TodoTemplateEntity, Guid>(request.ById).AsEnumerable().Select(entity => entity.ToDTO()));
    }
}