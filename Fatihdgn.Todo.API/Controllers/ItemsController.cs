using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Models;
using Fatihdgn.Todo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.API.Controllers;

[ApiController]
public class ItemsController : Controller
{
    private readonly ITodoItemRepository _repo;

    public ItemsController(ITodoItemRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get() => Ok(_repo.GetAll());

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _repo.FindAsync(id);
        return result.Match<IActionResult>(
            Ok,
            notFound => NotFound()
        );
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(TodoItem model)
    {
        var result = await _repo.AddAsync(new TodoItemEntity
        {
            Id = Guid.NewGuid(),
            Content = model.Content,
            Note = model.Note,
            DueAt = model.DueAt,
            RemindAt = model.RemindAt,
        });
        return Ok(result.AsT0);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(Guid id, TodoItem model)
    {
        var result = await _repo.FindAsync(id);
        return await result.Match<Task<IActionResult>>(
            async entity =>
            {
                entity.Note = model.Note;
                entity.DueAt = model.DueAt;
                entity.Content = model.Content;
                entity.RemindAt = model.RemindAt;
                var updateResult = await _repo.UpdateAsync(entity);
                return updateResult.Match<IActionResult>(Ok, _ => BadRequest("Error happened when updating"));
            },
            _ => Task.FromResult((IActionResult)NotFound())
        );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return (await _repo.RemoveAsync(id)).Match<IActionResult>(
            _ => Ok(), 
            _ => NotFound()
        );
    }
}
