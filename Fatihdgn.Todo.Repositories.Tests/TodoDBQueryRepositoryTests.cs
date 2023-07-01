using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Repositories.Tests;

public class TodoDBQueryRepositoryTests
{
    private readonly TodoDB _context;
    private readonly TodoDBQueryRepository<TodoItemEntity, Guid> sut;

    public TodoDBQueryRepositoryTests()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase(nameof(TodoDB)).Options;
        _context = new TodoDB(options);
        sut = new TodoDBQueryRepository<TodoItemEntity, Guid>(_context);
    }

    async Task ClearEntities()
    {
        _context.Items.RemoveRange(_context.Items);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task ByIdAsync_WithRandomId_ReturnsNotFound()
    {
        var result = await sut.ByIdAsync(Guid.NewGuid());
        result.IsT1.Should().BeTrue();
    }

    [Fact]
    public async Task ByIdAsync_WithValidId_ReturnsEntity()
    {
        var id = Guid.NewGuid();
        var entry = await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = id });
        await _context.SaveChangesAsync();

        var result = await sut.ByIdAsync(id);

        result.IsT0.Should().BeTrue();
        result.AsT0.Id.Should().Be(id);

        await ClearEntities();

    }

    [Fact]
    public async Task AsQueryable_WithEmptyStore_ReturnsNone()
    {
        await ClearEntities();
        var result = await sut.AsQueryable().ToListAsync();
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task AsQueryable_WithFiveEntities_ReturnsFiveEntities()
    {
        await Task.WhenAll(Enumerable.Range(1, 5).Select(async i =>
            await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = Guid.NewGuid(), Content = i.ToString() })
        ));
        await _context.SaveChangesAsync();

        var result = await sut.AsQueryable().ToListAsync();
        result.Count.Should().Be(5);
        await ClearEntities();
    }

    [Fact]
    public async Task Where_WithEmptyStore_ReturnsNone()
    {
        var result = await sut.AsQueryable().Where(x => x.Content.Contains("content")).ToListAsync();
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task Where_WithValidQueryAndFiveEntities_ReturnsFiveEntities()
    {
        await Task.WhenAll(Enumerable.Range(1, 5).Select(async i =>
            await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = Guid.NewGuid(), Content = $"{i}. content" })
        ));
        await _context.SaveChangesAsync();

        var result = await sut.AsQueryable().Where(x => x.Content.Contains("content")).ToListAsync();

        result.Count.Should().Be(5);
        await ClearEntities();
    }

    [Fact]
    public async Task Where_WithValidQueryMatchingThreeEntities_ReturnsThreeEntities()
    {
        await Task.WhenAll(Enumerable.Range(1, 5).Select(async i =>
            await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = Guid.NewGuid(), Content = $"{i}. content" })
        ));
        await _context.SaveChangesAsync();

        var result = await sut.AsQueryable().Where(x => x.Content.StartsWith("1") || x.Content.StartsWith("2") || x.Content.StartsWith("3")).ToListAsync();

        result.Count.Should().Be(3);
        await ClearEntities();
    }
}