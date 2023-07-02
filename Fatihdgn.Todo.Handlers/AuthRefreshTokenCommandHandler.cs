using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Extensions;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fatihdgn.Todo.Handlers;

public class AuthRefreshTokenCommandHandler : IRequestHandler<AuthRefreshTokenCommand, OneOf<AuthRefreshTokenResponseDTO, Error<string>>>
{
    private readonly UserManager<TodoUserEntity> _userManager;
    private readonly IConfiguration _config;

    public AuthRefreshTokenCommandHandler(UserManager<TodoUserEntity> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<OneOf<AuthRefreshTokenResponseDTO, Error<string>>> Handle(AuthRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Model.Email);
        if (user == null || user.RefreshToken != request.Model.RefreshToken) return new Error<string>("Invalid refresh token");


        user.RenewRefreshToken();
        await _userManager.UpdateAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["JwtBearerAuthenticationIssuerSigningKey"]);

        // Generate new access token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _config["JwtBearerAuthenticationValidAudience"],
            Issuer = _config["JwtBearerAuthenticationValidIssuer"],
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessTokenString = tokenHandler.WriteToken(accessToken);

        // Refresh token successful, return the new access token
        return new AuthRefreshTokenResponseDTO(accessTokenString, user.RefreshToken);
    }
}
