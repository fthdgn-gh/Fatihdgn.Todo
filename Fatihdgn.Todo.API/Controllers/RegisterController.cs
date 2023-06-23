using Fatihdgn.Todo.API.Models;
using Fatihdgn.Todo.Entities;
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["JwtBearerAuthenticationIssuerSigningKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Login successful, return the JWT token
                return Ok(new { Token = tokenString });
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
}
