using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct AuthRefreshTokenCommand(AuthRefreshTokenDTO Model) : IRequest<OneOf<AuthRefreshTokenResponseDTO, Error<string>>>;