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
[Route("api/items")]
public class ItemsController : Controller
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("by/lists/{id}")]
    [OpenApiOperation("GetAllItemsByListId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TodoItemDTO>))]
    public async Task<IActionResult> GetAllByListId(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetAllTodoItemsByListIdQuery(userId, id));
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [OpenApiOperation("GetItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetTodoItemQuery(userId, id));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound()
        );
    }

    [HttpPost]
    [Route("")]
    [OpenApiOperation("CreateItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Create([FromBody] TodoItemCreateDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new CreateTodoItemCommand(userId, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => BadRequest("Couldn't find a the user."),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPut]
    [Route("{id}")]
    [OpenApiOperation("UpdateItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoItemUpdateDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new UpdateTodoItemCommand(userId, id, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPatch]
    [Route("{id}")]
    [OpenApiOperation("PatchItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Patch(Guid id, [FromBody] TodoItemPatchDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new PatchTodoItemCommand(userId, id, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpDelete]
    [Route("{id}")]
    [OpenApiOperation("RemoveItem")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new RemoveTodoItemCommand(userId, id));
        return response.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound()
        );
    }
}
