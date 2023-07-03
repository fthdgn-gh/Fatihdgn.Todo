using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Text.Json;

namespace Fatihdgn.Todo.Handlers;

public class CreateTodoTemplateByListCommandHandler : IRequestHandler<CreateTodoTemplateByListCommand, OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoTemplateRepository _repo;
    private readonly ITodoListQueryRepository _listQuery;
    private readonly ITodoUserQueryRepository _userQuery;

    public CreateTodoTemplateByListCommandHandler(ITodoTemplateRepository repo, ITodoListQueryRepository listQuery, ITodoUserQueryRepository userQuery)
    {
        _repo = repo;
        _listQuery = listQuery;
        _userQuery = userQuery;
    }

    public async Task<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>> Handle(CreateTodoTemplateByListCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.ById)) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.ById)));

        var userResult = await _userQuery.ByIdAsync(request.ById);
        if (userResult.IsT1) return userResult.AsT1;
        var user = userResult.AsT0;

        var listResult = await _listQuery.ByIdAsync(request.ListId);
        if (listResult.IsT1) return listResult.AsT1;
        var template = listResult.AsT0;

        var contents = template.Items.Select(x => x.Content).ToList();
        var entity = new TodoTemplateEntity { Id = Guid.NewGuid(), By = user };

        entity.Contents = contents;

        var addResult = await _repo.AddAsync(entity);

        if (addResult.IsT1) return addResult.AsT1;

        return addResult.AsT0.ToDTO();
    }
}