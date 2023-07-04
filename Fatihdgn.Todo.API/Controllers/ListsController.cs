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
[Route("api/v{version:apiVersion}/lists")]
[ApiVersion("1.0")]
public class ListsController : Controller
{
    private readonly IMediator _mediator;

    public ListsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    [OpenApiOperation("GetAllLists")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TodoListDTO>))]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetAllTodoListsQuery(userId));
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [OpenApiOperation("GetList")]
    [ProducesResponseType(200, Type = typeof(TodoListDTO))]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetTodoListQuery(userId, id));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound()
        );
    }

    [HttpGet]
    [Route("{id}/items")]
    [OpenApiOperation("GetListItems")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TodoItemDTO>))]
    public async Task<IActionResult> GetItems(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new GetAllTodoItemsByListIdQuery(userId, id));
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [OpenApiOperation("CreateList")]
    [ProducesResponseType(200, Type = typeof(TodoListDTO))]
    public async Task<IActionResult> Create([FromBody] TodoListCreateDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new CreateTodoListCommand(userId, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => BadRequest("Couldn't find a the user."),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPost]
    [Route("generate/templates/{id}")]
    [OpenApiOperation("CreateListByTemplate")]
    [ProducesResponseType(200, Type = typeof(TodoListDTO))]
    public async Task<IActionResult> CreateByTemplate(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new CreateTodoListByTemplateCommand(userId, id));
        return response.Match<IActionResult>(
            Ok,
            notFound => BadRequest("Couldn't find a the template."),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPut]
    [Route("{id}")]
    [OpenApiOperation("UpdateList")]
    [ProducesResponseType(200, Type = typeof(TodoListDTO))]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoListUpdateDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new UpdateTodoListCommand(userId, id, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPatch]
    [Route("{id}")]
    [OpenApiOperation("PatchList")]
    [ProducesResponseType(200, Type = typeof(TodoListDTO))]
    public async Task<IActionResult> Patch(Guid id, [FromBody] TodoListPatchDTO model)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new PatchTodoListCommand(userId, id, model));
        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpDelete]
    [Route("{id}")]
    [OpenApiOperation("RemoveList")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var userId = User.GetNameIdentifier();
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _mediator.Send(new RemoveTodoListCommand(userId, id));
        return response.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound()
        );
    }
}
