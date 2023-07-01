using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;

namespace Fatihdgn.Todo.Handlers;

public class GetAllTodoItemsByListIdQueryHandler : IRequestHandler<GetAllTodoItemsByListIdQuery, IEnumerable<TodoItemDTO>>
{
    private readonly ITodoItemRepository _repo;

    public GetAllTodoItemsByListIdQueryHandler(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<TodoItemDTO>> Handle(GetAllTodoItemsByListIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repo.AsQueryable().ByUserId(request.ById).ByListId(request.ListId).AsEnumerable().Select(entity => entity.ToDTO()));
    }
}