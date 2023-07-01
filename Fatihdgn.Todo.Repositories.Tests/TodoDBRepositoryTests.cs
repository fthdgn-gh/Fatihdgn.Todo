using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;
using FluentAssertions;
using Moq;
using OneOf;
using OneOf.Types;
using System.Linq.Expressions;

namespace Fatihdgn.Todo.Repositories.Tests
{
    public class TodoDBRepositoryTests
    {
        private readonly Mock<ICommandRepository<TodoItemEntity, Guid>> _mockCommandRepository;
        private readonly Mock<IQueryRepository<TodoItemEntity, Guid>> _mockQueryRepository;
        private readonly TodoDBRepository<TodoItemEntity, Guid> sut;

        public TodoDBRepositoryTests()
        {
            _mockCommandRepository = new Mock<ICommandRepository<TodoItemEntity, Guid>>();
            _mockQueryRepository = new Mock<IQueryRepository<TodoItemEntity, Guid>>();
            sut = new TodoDBRepository<TodoItemEntity, Guid>(_mockCommandRepository.Object, _mockQueryRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldCall_AddAsyncWithinCommand()
        {
            _mockCommandRepository.Setup(x => x.AddAsync(It.IsAny<TodoItemEntity>()));

            await sut.AddAsync(new TodoItemEntity());

            _mockCommandRepository.Verify(x => x.AddAsync(It.IsAny<TodoItemEntity>()), Times.Once());
        }

        [Fact]
        public async Task AsQueryable_ShouldCall_AsQueryableWithinQuery()
        {
            _mockQueryRepository.Setup(x => x.AsQueryable());

            var result = sut.AsQueryable();

            _mockQueryRepository.Verify(x => x.AsQueryable(), Times.Once());
        }

        [Fact]
        public async Task ById_ShouldCall_ByIdWithinQuery()
        {
            _mockQueryRepository.Setup(x => x.ById(It.IsAny<Guid>()));

            var result = await sut.ById(Guid.NewGuid());

            _mockQueryRepository.Verify(x => x.ById(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task RemoveAsync_ShouldCall_RemoveAsyncWithinCommand()
        {
            _mockCommandRepository.Setup(x => x.RemoveAsync(It.IsAny<TodoItemEntity>()));

            await sut.RemoveAsync(new TodoItemEntity());

            _mockCommandRepository.Verify(x => x.RemoveAsync(It.IsAny<TodoItemEntity>()), Times.Once());
        }

        [Fact]
        public async Task RemoveAsyncWithGuid_ShouldCall_RemoveAsyncWithGuidWithinCommand()
        {
            _mockCommandRepository.Setup(x => x.RemoveAsync(It.IsAny<Guid>()));

            await sut.RemoveAsync(Guid.NewGuid());

            _mockCommandRepository.Verify(x => x.RemoveAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_ShouldCall_UpdateAsyncWithinCommand()
        {
            _mockCommandRepository.Setup(x => x.UpdateAsync(It.IsAny<TodoItemEntity>()));

            await sut.UpdateAsync(new TodoItemEntity());

            _mockCommandRepository.Verify(x => x.UpdateAsync(It.IsAny<TodoItemEntity>()), Times.Once());
        }
    }
}
