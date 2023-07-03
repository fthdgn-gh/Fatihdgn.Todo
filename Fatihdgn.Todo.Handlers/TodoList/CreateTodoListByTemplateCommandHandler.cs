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

public class CreateTodoListByTemplateCommandHandler : IRequestHandler<CreateTodoListByTemplateCommand, OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoListRepository _repo;
    private readonly ITodoTemplateQueryRepository _templateQuery;
    private readonly ITodoUserQueryRepository _userQuery;

    public CreateTodoListByTemplateCommandHandler(ITodoListRepository repo, ITodoTemplateQueryRepository templateQuery, ITodoUserQueryRepository userQuery)
    {
        _repo = repo;
        _templateQuery = templateQuery;
        _userQuery = userQuery;
    }

    public async Task<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>> Handle(CreateTodoListByTemplateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.ById)) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.ById)));

        var userResult = await _userQuery.ByIdAsync(request.ById);
        if (userResult.IsT1) return userResult.AsT1;
        var user = userResult.AsT0;

        var templateResult = await _templateQuery.ByIdAsync(request.TemplateId);
        if (templateResult.IsT1) return templateResult.AsT1;
        var template = templateResult.AsT0;

        var contents = template.Contents.Value ?? new List<string>();
        var entity = new TodoListEntity { Id = Guid.NewGuid(), By = user };

        foreach (var content in contents)
            entity.Items.Add(new TodoItemEntity { Id = Guid.NewGuid(), By = user, Content = content ?? string.Empty });

        var addResult = await _repo.AddAsync(entity);

        if (addResult.IsT1) return addResult.AsT1;

        return addResult.AsT0.ToDTO();
    }
}