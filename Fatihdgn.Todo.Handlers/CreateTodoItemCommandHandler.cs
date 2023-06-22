using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.DTOs.Mappings.Entities;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, OneOf<TodoItemDTO, Error<ArgumentNullException>>>
{
    private readonly ITodoItemRepository _repo;

    public CreateTodoItemCommandHandler(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<TodoItemDTO, Error<ArgumentNullException>>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Model == null) return new Error<ArgumentNullException>(new ArgumentNullException(nameof(request.Model)));
        return (
            await _repo.AddAsync(
                request.Model.ApplyTo(new TodoItemEntity { Id = Guid.NewGuid() })
            )
        ).MapT0(entity => entity.ToDTO());
    }
}