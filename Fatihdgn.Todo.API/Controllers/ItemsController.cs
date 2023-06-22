using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.DTOs;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Models;
using Fatihdgn.Todo.Repositories;
using Fatihdgn.Todo.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.API.Controllers;

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
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllTodoItemsQuery());
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
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
    public async Task<IActionResult> Post(TodoItemCreateDTO model)
    {
        var response = await _mediator.Send(new CreateTodoItemCommand(model));
        return response.Match<IActionResult>(
            Ok,
            error => BadRequest(error.Value.Message)
        );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(Guid id, TodoItemUpdateDTO model)
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
    public async Task<IActionResult> Patch(Guid id, TodoItemPatchDTO model)
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
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new RemoveTodoItemCommand(id));
        return response.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound()
        );
    }
}
