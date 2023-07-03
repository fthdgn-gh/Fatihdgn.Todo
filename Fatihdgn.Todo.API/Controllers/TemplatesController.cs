using Fatihdgn.Todo.API.Extensions;
using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Fatihdgn.Todo.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/v{version:apiVersion}/templates")]
[ApiVersion("1.0")]
public class TemplatesController : Controller
{
    private readonly IMediator _mediator;

    public TemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    [OpenApiOperation("GetAllTemplates")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TodoTemplateDTO>))]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetAllTodoTemplatesQuery(userId));
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [OpenApiOperation("GetTemplate")]
    [ProducesResponseType(200, Type = typeof(TodoTemplateDTO))]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetTodoTemplateQuery(userId, id));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound()
        );
    }

    [HttpPost]
    [Route("")]
    [OpenApiOperation("CreateTemplate")]
    [ProducesResponseType(200, Type = typeof(TodoTemplateDTO))]
    public async Task<IActionResult> Create([FromBody] TodoTemplateCreateDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new CreateTodoTemplateCommand(userId, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => BadRequest("Couldn't find a the user."),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPost]
    [Route("by/lists/{id}")]
    [OpenApiOperation("CreateTemplateByList")]
    [ProducesResponseType(200, Type = typeof(TodoTemplateDTO))]
    public async Task<IActionResult> CreateByList(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new CreateTodoTemplateByListCommand(userId, id));
        return response.Match<IActionResult>(
            Ok,
            notFound => BadRequest("Couldn't find a the template."),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPut]
    [Route("{id}")]
    [OpenApiOperation("UpdateTemplate")]
    [ProducesResponseType(200, Type = typeof(TodoTemplateDTO))]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoTemplateUpdateDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new UpdateTodoTemplateCommand(userId, id, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPatch]
    [Route("{id}")]
    [OpenApiOperation("PatchTemplate")]
    [ProducesResponseType(200, Type = typeof(TodoTemplateDTO))]
    public async Task<IActionResult> Patch(Guid id, [FromBody] TodoTemplatePatchDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new PatchTodoTemplateCommand(userId, id, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpDelete]
    [Route("{id}")]
    [OpenApiOperation("RemoveTemplate")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new RemoveTodoTemplateCommand(userId, id));
        return response.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound()
        );
    }
}
