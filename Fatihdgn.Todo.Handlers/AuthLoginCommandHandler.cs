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

public class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, OneOf<AuthLoginResponseDTO, Error<string>>>
{
    private readonly UserManager<TodoUserEntity> _userManager;
    private readonly IConfiguration _config;

    public AuthLoginCommandHandler(UserManager<TodoUserEntity> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<OneOf<AuthLoginResponseDTO, Error<string>>> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, request.Model.Password))
        {
            user.RenewRefreshToken();
            await _userManager.UpdateAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JwtBearerAuthenticationIssuerSigningKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config["JwtBearerAuthenticationValidAudience"],
                Issuer = _config["JwtBearerAuthenticationValidIssuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = request.Model.RememberMe ? DateTime.UtcNow.AddHours(2) : DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            if (request.Model.RememberMe)
            {
                return new AuthLoginResponseDTO(accessToken, user.RefreshToken);
            }
            else
                return new AuthLoginResponseDTO(accessToken);
        }
        else
        {
            // Invalid email or password
            return new Error<string>("Invalid email or password");
        }
    }
}
