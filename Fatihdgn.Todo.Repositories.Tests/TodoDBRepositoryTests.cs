using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Fatihdgn.Todo.Repositories.Tests
{
    public class TodoDBRepositoryTests
    {
        private readonly TodoDB _context;
        private readonly TodoDBRepository<TodoItemEntity> sut;

        public TodoDBRepositoryTests()
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(nameof(TodoDB)).Options;
            _context = new TodoDB(options);
            sut = new TodoDBRepository<TodoItemEntity>(_context);
        }

        async Task ClearEntities()
        {
            _context.Items.RemoveRange(_context.Items);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task FindAsync_WithRandomId_ReturnsNotFound()
        {
            var result = await sut.FindAsync(Guid.NewGuid());
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task FindAsync_WithValidId_ReturnsEntity()
        {
            var id = Guid.NewGuid();
            await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = id });
            await _context.SaveChangesAsync();

            var result = await sut.FindAsync(id);
            result.IsT0.Should().BeTrue();
            result.AsT0.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetAll_WithEmptyStore_ReturnsNone()
        {
            var result = await sut.GetAll().ToListAsync();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAll_WithFiveEntities_ReturnsFiveEntities()
        {
            await Task.WhenAll(Enumerable.Range(1, 5).Select(async i => 
                await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = Guid.NewGuid(), Content = i.ToString() })
            ));
            await _context.SaveChangesAsync();
            
            var result = await sut.GetAll().ToListAsync();
            result.Count.Should().Be(5);
            await ClearEntities();
        }

        [Fact]
        public async Task Where_WithEmptyStore_ReturnsNone()
        {
            var result = await sut.Where(x => x.Content.Contains("content")).ToListAsync();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task Where_WithValidQueryAndFiveEntities_ReturnsFiveEntities()
        {
            await Task.WhenAll(Enumerable.Range(1, 5).Select(async i =>
                await _context.Items.AddAsync(new Entities.TodoItemEntity { Id = Guid.NewGuid(), Content = $"{i}. content" })
            ));
            await _context.SaveChangesAsync();

            var result = await sut.Where(x => x.Content.Contains("content")).ToListAsync();
            
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

            var result = await sut.Where(x => x.Content.StartsWith("1") || x.Content.StartsWith("2") || x.Content.StartsWith("3")).ToListAsync();
            
            result.Count.Should().Be(3);
            await ClearEntities();
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
            var result = await sut.AddAsync(new Entities.TodoItemEntity() { Id = id });
            result.IsT0.Should().BeTrue();
            result.AsT0.Id.Should().Be(id);
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
            var entry = await _context.Items.AddAsync(new Entities.TodoItemEntity() { Id = id });
            await _context.SaveChangesAsync();
            var entity = entry.Entity;

            entity.Content = content;
            var updateResult = await sut.UpdateAsync(entity);

            updateResult.IsT0.Should().BeTrue();
            var result = updateResult.AsT0;
            result.Should().BeEquivalentTo(entity);
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
            var entry = await _context.Items.AddAsync(new Entities.TodoItemEntity() { Id = id });
            await _context.SaveChangesAsync();
            var entity = entry.Entity;

            var result = await sut.RemoveAsync(entity);
            result.IsT0.Should().BeTrue();
            result.AsT0.Should().BeEquivalentTo(entity);
        }
    }
}