using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fatihdgn.Todo.API.Controllers;

[ApiController]
public class ItemsController : Controller
{
    private readonly TodoContext _context;

    public ItemsController(TodoContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _context.Items.FindAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TodoItem model)
    {
        var entry = await _context.Items.AddAsync(new Entities.TodoItemEntity
        {
            Id = Guid.NewGuid(),
            Content = model.Content,
            Note = model.Note,
            DueAt = model.DueAt,
            RemindAt = model.RemindAt,
        });
        return Ok(entry.Entity);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Post(Guid id, TodoItem model)
    {
        var result = await _context.Items.FindAsync(id);
        if (result == null) return NotFound();
        result.Note = model.Note;
        result.DueAt = model.DueAt;
        result.Content = model.Content;
        result.RemindAt = model.RemindAt;
        await _context.SaveChangesAsync();
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = new TodoItemEntity() { Id = id };
        _context.Items.Attach(result);
        _context.Items.Remove(result);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
