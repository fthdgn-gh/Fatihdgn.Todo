using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

internal class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, OneOf<None, Error<IEnumerable<IdentityError>>>>
{
    const string DefaultListName = "Todo List";

    private readonly UserManager<TodoUserEntity> _userManager;
    private readonly ITodoListCommandRepository _todoListCommandRepository;

    public AuthRegisterCommandHandler(UserManager<TodoUserEntity> userManager, ITodoListCommandRepository todoListCommandRepository)
    {
        _userManager = userManager;
        _todoListCommandRepository = todoListCommandRepository;
    }

    public async Task<OneOf<None, Error<IEnumerable<IdentityError>>>> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new TodoUserEntity { UserName = request.Model.Email, Email = request.Model.Email };
        user.RenewRefreshToken();

        var result = await _userManager.CreateAsync(user, request.Model.Password);
        if (!result.Succeeded) return new Error<IEnumerable<IdentityError>>(result.Errors);

        var defaultListResult = await _todoListCommandRepository.AddAsync(new TodoListEntity { Id = Guid.NewGuid(), By = user, Name = DefaultListName });
        var defaultList = defaultListResult.AsT0;

        return new None();
    }
}
