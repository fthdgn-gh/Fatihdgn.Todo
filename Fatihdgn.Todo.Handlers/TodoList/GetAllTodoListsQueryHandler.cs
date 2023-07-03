using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using Fatihdgn.Todo.Entities.Extensions;

namespace Fatihdgn.Todo.Handlers;

public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, IEnumerable<TodoListDTO>>
{
    private readonly ITodoListRepository _repo;

    public GetAllTodoListsQueryHandler(ITodoListRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<TodoListDTO>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repo.AsQueryable().ByUserId(request.ById).AsEnumerable().Select(entity => entity.ToDTO()));
    }
}