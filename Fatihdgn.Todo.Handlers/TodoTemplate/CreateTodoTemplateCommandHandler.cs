using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class CreateTodoTemplateCommandHandler : IRequestHandler<CreateTodoTemplateCommand, OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoTemplateRepository _repo;
    private readonly ITodoUserQueryRepository _userQuery;

    public CreateTodoTemplateCommandHandler(ITodoTemplateRepository repo, ITodoUserQueryRepository userQuery)
    {
        _repo = repo;
        _userQuery = userQuery;
    }

    public async Task<OneOf<TodoTemplateDTO, NotFound, Error<ArgumentNullException>>> Handle(CreateTodoTemplateCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.Model)));
        if (string.IsNullOrEmpty(request.ById)) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.ById)));

        var userResult = await _userQuery.ByIdAsync(request.ById);
        if (userResult.IsT1) return userResult.AsT1;

        var entity = request.Model.ApplyTo(new TodoTemplateEntity { Id = Guid.NewGuid(), By = userResult.AsT0 });

        var addResult = await _repo.AddAsync(entity);

        if (addResult.IsT1) return addResult.AsT1;

        return addResult.AsT0.ToDTO();
    }
}