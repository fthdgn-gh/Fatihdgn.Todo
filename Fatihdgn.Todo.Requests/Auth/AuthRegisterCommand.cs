using Fatihdgn.Todo.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct AuthRegisterCommand(AuthRegisterDTO Model) : IRequest<OneOf<None, Error<IEnumerable<IdentityError>>>>;