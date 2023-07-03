using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>>
{
    private readonly ITodoItemRepository _repo;
    private readonly ITodoListQueryRepository _listQuery;
    private readonly ITodoUserQueryRepository _userQuery;

    public CreateTodoItemCommandHandler(ITodoItemRepository repo, ITodoListQueryRepository listQuery, ITodoUserQueryRepository userQuery)
    {
        _repo = repo;
        _listQuery = listQuery;
        _userQuery = userQuery;
    }

    public async Task<OneOf<TodoItemDTO, NotFound, Error<ArgumentNullException>>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.Model)));
        if (string.IsNullOrEmpty(request.ById)) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.ById)));

        var userResult = await _userQuery.ByIdAsync(request.ById);
        if (userResult.IsT1) return userResult.AsT1;
        var user = userResult.AsT0;

        var listResult = await _listQuery.ByIdAsync(request.Model.ListId);
        if (listResult.IsT1) return listResult.AsT1;
        var list = listResult.AsT0;

        var entity = request.Model.ApplyTo(new TodoItemEntity { Id = Guid.NewGuid(), By = user, List = list });

        var addResult = await _repo.AddAsync(entity);

        if (addResult.IsT1) return addResult.AsT1;

        return addResult.AsT0.ToDTO();
    }
}