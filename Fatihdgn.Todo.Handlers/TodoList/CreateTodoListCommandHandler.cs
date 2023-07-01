using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoListRepository _repo;
    private readonly ITodoUserQueryRepository _userQuery;

    public CreateTodoListCommandHandler(ITodoListRepository repo, ITodoUserQueryRepository userQuery)
    {
        _repo = repo;
        _userQuery = userQuery;
    }

    public async Task<OneOf<TodoListDTO, NotFound, Error<ArgumentNullException>>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.Model)));
        if (string.IsNullOrEmpty(request.ById)) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.ById)));

        var userResult = await _userQuery.ByIdAsync(request.ById);
        if (userResult.IsT1) return userResult.AsT1;

        var entity = request.Model.ApplyTo(new TodoListEntity { Id = Guid.NewGuid(), By = userResult.AsT0 });
        
        var addResult = await _repo.AddAsync(entity);

        if (addResult.IsT1) return addResult.AsT1;

        return addResult.AsT0.ToDTO();
    }
}