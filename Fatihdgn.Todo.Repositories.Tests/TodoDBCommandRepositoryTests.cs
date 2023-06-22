using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Fatihdgn.Todo.Repositories.Tests;

public class TodoDBCommandRepositoryTests
{
    private readonly TodoDB _context;
    private readonly TodoDBCommandRepository<TodoItemEntity> sut;

    public TodoDBCommandRepositoryTests()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase(nameof(TodoDB)).Options;
        _context = new TodoDB(options);
        sut = new TodoDBCommandRepository<TodoItemEntity>(_context);
    }

    async Task ClearEntities()
    {
        _context.Items.RemoveRange(_context.Items);
        await _context.SaveChangesAsync();
    }


    [Fact]
    public async Task AddAsync_WithNullParameter_ReturnsError()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = await sut.AddAsync(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        result.IsT1.Should().BeTrue();
        result.AsT1.Value.Should().BeOfType<ArgumentNullException>();
    }

    [Fact]
    public async Task AddAsync_WithValidParameter_ReturnsEntity()
    {
        var id = Guid.NewGuid();

        var result = await sut.AddAsync(new TodoItemEntity() { Id = id, Content = "To be added" });

        result.IsT0.Should().BeTrue();
        result.AsT0.Id.Should().Be(id);

        _context.Remove(result.AsT0);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateAsync_WithNullParameter_ReturnsError()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = await sut.UpdateAsync(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        result.IsT1.Should().BeTrue();
        result.AsT1.Value.Should().BeOfType<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateAsync_WithValidParameter_ReturnsEntity()
    {
        var id = Guid.NewGuid();
        var content = "Hello there";
        var entry = await _context.Items.AddAsync(new TodoItemEntity() { Id = id, Content = "To be updated" });
        await _context.SaveChangesAsync();
        var entity = entry.Entity;

        entity.Content = content;
        var updateResult = await sut.UpdateAsync(entity);

        updateResult.IsT0.Should().BeTrue();
        var result = updateResult.AsT0;

        result.Id.Should().Be(id);
        result.Content.Should().Be(content);
        await ClearEntities();
    }

    [Fact]
    public async Task RemoveAsync_WithNullParameter_ReturnsError()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = await sut.RemoveAsync(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        result.IsT1.Should().BeTrue();
        result.AsT1.Value.Should().BeOfType<ArgumentNullException>();
    }

    [Fact]
    public async Task RemoveAsync_WithValidParameter_ReturnsEntity()
    {
        var id = Guid.NewGuid();
        var entry = await _context.Items.AddAsync(new Entities.TodoItemEntity() { Id = id, Content = "To be removed" });
        await _context.SaveChangesAsync();
        var entity = entry.Entity;

        var result = await sut.RemoveAsync(entity);
        result.IsT0.Should().BeTrue();
        result.AsT0.Should().BeEquivalentTo(entity);
        await ClearEntities();
    }

    [Fact]
    public async Task RemoveAsync_WithValidId_ReturnsNone()
    {
        var id = Guid.NewGuid();
        var entry = await _context.Items.AddAsync(new Entities.TodoItemEntity() { Id = id, Content = "To be removed" });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var result = await sut.RemoveAsync(id);

        result.IsT0.Should().BeTrue();
        result.AsT0.Should().BeOfType<None>();

        await ClearEntities();
    }

    [Fact]
    public async Task RemoveAsync_WithInvalidId_ReturnsNotFound()
    {
        var id = Guid.NewGuid();

        var result = await sut.RemoveAsync(id);

        result.IsT1.Should().BeTrue();
        result.AsT1.Should().BeOfType<NotFound>();
    }

}