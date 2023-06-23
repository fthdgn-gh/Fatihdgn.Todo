using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Fatihdgn.Todo.API.Controllers;

[Authorize]
[ApiController]
public class ItemsController : Controller
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    [OpenApiOperation("GetAllItems")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TodoItemDTO>))]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllTodoItemsQuery());
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [OpenApiOperation("GetItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _mediator.Send(new GetTodoItemQuery(id));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound()
        );
    }

    [HttpPost]
    [Route("")]
    [OpenApiOperation("CreateItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Create([FromBody]TodoItemCreateDTO model)
    {
        var response = await _mediator.Send(new CreateTodoItemCommand(model));
        return response.Match<IActionResult>(
            Ok,
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPut]
    [Route("{id}")]
    [OpenApiOperation("UpdateItem")]
    [ProducesResponseType(200, Type = typeof(TodoItemDTO))]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoItemUpdateDTO model)
    {
        var response = await _mediator.Send(new UpdateTodoItemCommand(id, model));
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
        var response = await _mediator.Send(new PatchTodoItemCommand(id, model));
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
        var response = await _mediator.Send(new RemoveTodoItemCommand(id));
        return response.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound()
        );
    }
}
