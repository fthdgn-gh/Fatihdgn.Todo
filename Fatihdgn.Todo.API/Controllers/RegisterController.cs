using Fatihdgn.Todo.API.Models;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Entities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fatihdgn.Todo.API.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<TodoUserEntity> _userManager;
    private readonly IConfiguration _config;

    public AuthController(UserManager<TodoUserEntity> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                user.RenewRefreshToken();
                await _userManager.UpdateAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["JwtBearerAuthenticationIssuerSigningKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                    }),
                    Expires = model.RememberMe ? DateTime.UtcNow.AddHours(2) : DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(token);

                if (model.RememberMe)
                {
                    return Ok(new { AccessToken = accessToken, RefreshToken = user.RefreshToken });
                }
                else
                    return Ok(new { AccessToken = accessToken });
            }
            else
            {
                // Invalid email or password
                return BadRequest("Invalid email or password");
            }
        }

        // Invalid login model
        return BadRequest(ModelState);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new TodoUserEntity { UserName = model.Email, Email = model.Email };
            user.RenewRefreshToken();
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User registration successful
                return Ok();
            }
            else
            {
                // User registration failed, return errors
                return BadRequest(result.Errors);
            }
        }

        // Invalid registration model
        return BadRequest(ModelState);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && user.RefreshToken == model.RefreshToken)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["JwtBearerAuthenticationIssuerSigningKey"]);

                // Generate new access token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                        // Add any additional claims as needed
                    }),
                    Expires = DateTime.UtcNow.AddHours(2), // Set the new access token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var accessToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessTokenString = tokenHandler.WriteToken(accessToken);

                // Refresh token successful, return the new access token
                return Ok(new { AccessToken = accessTokenString, RefreshToken = user.RefreshToken });
            }
            else
            {
                // Invalid refresh token
                return BadRequest("Invalid refresh token");
            }
        }

        // Invalid refresh token model
        return BadRequest(ModelState);
    }
}
