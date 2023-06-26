using Fatihdgn.Todo.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct AuthLoginCommand(AuthLoginDTO Model) : IRequest<OneOf<AuthLoginResponseDTO, Error<string>>>;
public record struct AuthRegisterCommand(AuthRegisterDTO Model) : IRequest<OneOf<None, Error<IEnumerable<IdentityError>>>>;
public record struct AuthRefreshTokenCommand(AuthRefreshTokenDTO Model) : IRequest<OneOf<AuthRefreshTokenResponseDTO, Error<string>>>;