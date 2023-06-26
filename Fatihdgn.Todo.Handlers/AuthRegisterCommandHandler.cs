using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Handlers;

internal class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, OneOf<None, Error<IEnumerable<IdentityError>>>>
{
    private readonly UserManager<TodoUserEntity> _userManager;

    public AuthRegisterCommandHandler(UserManager<TodoUserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<OneOf<None, Error<IEnumerable<IdentityError>>>> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new TodoUserEntity { UserName = request.Model.Email, Email = request.Model.Email };
        user.RenewRefreshToken();

        var result = await _userManager.CreateAsync(user, request.Model.Password);
        return result.Succeeded ? new None() : new Error<IEnumerable<IdentityError>>(result.Errors);
    }
}
