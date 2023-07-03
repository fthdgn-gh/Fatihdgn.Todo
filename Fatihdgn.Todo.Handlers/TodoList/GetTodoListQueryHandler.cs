using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;


namespace Fatihdgn.Todo.Handlers;

public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, OneOf<TodoListDTO, NotFound>>
{
    private readonly ITodoListRepository _repo;

    public GetTodoListQueryHandler(ITodoListRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<TodoListDTO, NotFound>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        var response = await _repo.AsQueryable().ByUserId(request.ById).ByIdAsync(request.Id);
        if (response == null) return new NotFound();
        return response.ToDTO();
    }
}