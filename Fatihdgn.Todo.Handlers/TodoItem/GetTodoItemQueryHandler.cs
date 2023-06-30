using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, OneOf<TodoItemDTO, NotFound>>
{
    private readonly ITodoItemRepository _repo;

    public GetTodoItemQueryHandler(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<TodoItemDTO, NotFound>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var response = await _repo.AsQueryable().ByUserId(request.ById).ByIdAsync(request.Id);
        if (response == null) return new NotFound();
        return response.ToDTO();
    }
}