using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fatihdgn.Todo.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthLoginResponseDTO))]
    [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> Login([FromBody] AuthLoginDTO model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _mediator.Send(new AuthLoginCommand(model));

        return response.Match<IActionResult>(
            response => Ok(response),
            error => BadRequest(error.Value)
        );
    }

    [HttpPost("register")]
    [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
    [ProducesResponseType(400, Type = typeof(IEnumerable<IdentityError>))]
    public async Task<IActionResult> Register([FromBody] AuthRegisterDTO model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _mediator.Send(new AuthRegisterCommand(model));

        return response.Match<IActionResult>(
            response => Ok(),
            error => BadRequest(error.Value)
        );
    }

    [HttpPost("refresh")]
    [ProducesResponseType(200, Type = typeof(AuthRefreshTokenResponseDTO))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
    public async Task<IActionResult> RefreshToken([FromBody] AuthRefreshTokenDTO model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _mediator.Send(new AuthRefreshTokenCommand(model));

        return response.Match<IActionResult>(
            response => Ok(response),
            error => BadRequest(error.Value)
        );
    }
}
