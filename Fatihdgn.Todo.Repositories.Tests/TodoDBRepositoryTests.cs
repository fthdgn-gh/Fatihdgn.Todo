using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;
using FluentAssertions;
using Moq;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.Repositories.Tests
{
    public class TodoDBRepositoryTests
    {
        private readonly Mock<ICommandRepository<TodoItemEntity>> _mockCommandRepository;
        private readonly Mock<IQueryRepository<TodoItemEntity>> _mockQueryRepository;
        private readonly TodoDBRepository<TodoItemEntity> sut;

        public TodoDBRepositoryTests()
        {
            _mockCommandRepository = new Mock<ICommandRepository<TodoItemEntity>>();
            _mockQueryRepository = new Mock<IQueryRepository<TodoItemEntity>>();
            sut = new TodoDBRepository<TodoItemEntity>(_mockCommandRepository.Object, _mockQueryRepository.Object);
        }


        [Fact]
        public async Task FindAsync_WithValidId_ReturnsEntity()
        {
            var id = Guid.NewGuid();
            var entity = new TodoItemEntity { Id = id };
            _mockQueryRepository.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(entity);

            var result = await sut.FindAsync(id);

            result.IsT0.Should().BeTrue();
            result.AsT0.Should().Be(entity);
        }

        [Fact]
        public async Task FindAsync_WithInvalidId_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            _mockQueryRepository.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(new NotFound());

            var result = await sut.FindAsync(id);

            result.IsT1.Should().BeTrue();
            result.AsT1.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetAll_ReturnsQueryRepositoryGetAll()
        {
            var expectedQuery = new List<TodoItemEntity>().AsQueryable();
            _mockQueryRepository.Setup(repo => repo.GetAll())
                .Returns(expectedQuery);

            var result = sut.GetAll();

            result.Should().BeSameAs(expectedQuery);
        }

        [Fact]
        public void Where_ReturnsQueryRepositoryWhere()
        {
            Expression<Func<TodoItemEntity, bool>> expectedExpression = entity => entity.Content.StartsWith("hello");
            var expectedQuery = new List<TodoItemEntity>().AsQueryable();
            _mockQueryRepository.Setup(repo => repo.Where(expectedExpression))
                .Returns(expectedQuery);

            var result = sut.Where(expectedExpression);

            result.Should().BeSameAs(expectedQuery);
        }

        [Fact]
        public async Task AddAsync_CallsCommandRepositoryAddAsync()
        {
            var entity = new TodoItemEntity();
            var expectedResponse = OneOf<TodoItemEntity, Error<ArgumentNullException>>.FromT0(entity);
            _mockCommandRepository.Setup(repo => repo.AddAsync(entity))
                .ReturnsAsync(expectedResponse);

            var result = await sut.AddAsync(entity);

            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task RemoveAsync_CallsCommandRepositoryRemoveAsync()
        {
            var entity = new TodoItemEntity();
            var expectedResponse = OneOf<TodoItemEntity, Error<ArgumentNullException>>.FromT0(entity);
            _mockCommandRepository.Setup(repo => repo.RemoveAsync(entity))
                .ReturnsAsync(expectedResponse);

            var result = await sut.RemoveAsync(entity);

            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task UpdateAsync_CallsCommandRepositoryUpdateAsync()
        {
            var entity = new TodoItemEntity();
            var expectedResponse = OneOf<TodoItemEntity, Error<ArgumentNullException>>.FromT0(entity);
            _mockCommandRepository.Setup(repo => repo.UpdateAsync(entity))
                .ReturnsAsync(expectedResponse);

            var result = await sut.UpdateAsync(entity);

            result.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
