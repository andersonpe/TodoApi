using Moq;
using TodoApi.Application.Dtos.Todo;
using TodoApi.Application.Services;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;
using TodoApi.Domain.Interfaces;

public class TodoServiceTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<ITodoRepository> _repo = new();
    private readonly TodoService _sut;

    public TodoServiceTests()
    {
        _uow.Setup(u => u.Todos).Returns(_repo.Object);
        _sut = new TodoService(_uow.Object);
    }

    [Fact]
    public async Task Create_PersistsAndCommits()
    {
        var dto = new CreateTodoDto("Estudar", null, TodoStatus.Pendente, DateTime.Today.AddDays(1));
        var result = await _sut.CreateAsync(dto);

        _repo.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Once);
        _uow.Verify(u => u.CommitAsync(), Times.Once);
        Assert.Equal("Estudar", result.Title);
    }

    [Fact]
    public async Task Update_ReturnsFalse_WhenNotFound()
    {
        _repo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((TodoItem?)null);
        var ok = await _sut.UpdateAsync(99, new UpdateTodoDto("x", null, TodoStatus.Concluido, DateTime.Today));
        Assert.False(ok);
    }

    [Fact]
    public async Task Delete_RemovesAndCommits_WhenFound()
    {
        var item = new TodoItem { Id = 1, Titulo = "a" };
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);

        Assert.True(await _sut.DeleteAsync(1));
        _repo.Verify(r => r.Remove(item), Times.Once);
        _uow.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAll_PassesFiltersToRepository()
    {
        _repo.Setup(r => r.GetAllAsync(TodoStatus.Pendente, null))
             .ReturnsAsync(new[] { new TodoItem { Id = 1, Titulo = "a" } });

        var list = await _sut.GetAllAsync(TodoStatus.Pendente, null);
        Assert.Single(list);
    }
}