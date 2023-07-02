using Fatihdgn.Todo.DTOs;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fatihdgn.Todo.Requests;

public record struct AuthLoginCommand(AuthLoginDTO Model) : IRequest<OneOf<AuthLoginResponseDTO, Error<string>>>;
