using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;

namespace Fatihdgn.Todo.Handlers;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IEnumerable<TodoItemDTO>>
{
    private readonly ITodoItemRepository _repo;

    public GetAllTodoItemsQueryHandler(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<TodoItemDTO>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repo.GetAll().AsEnumerable().Select(entity => entity.ToDTO()));
    }
}