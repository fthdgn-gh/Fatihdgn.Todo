using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Context.Tests;

public class TodoTemplateEntityTests
{
    private TodoDB _context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder().UseSqlite($"Data Source={nameof(TodoDB)}.db").Options;
        _context = new TodoDB(options);
    }

    [Test]
    public async Task CreateAndVerifyTheJsonContentField()
    {
        var contents = new List<string> { "Todo 1", "Todo 2", "Todo 3" };

        var result = await _context.Templates.AddAsync(new Entities.TodoTemplateEntity { Id = Guid.NewGuid(), Name = "Subject", Contents = contents });
        var entity = result.Entity;

        var deserializedContent = entity.Contents?.Value ?? new List<string>();

        deserializedContent.Should().BeEquivalentTo(contents);
    }
}