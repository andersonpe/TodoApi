using TodoApi.Application.Dtos;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;
using TodoApi.Domain.Interfaces;
namespace TodoApi.Application.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoResponseDto>> GetAllAsync(TodoStatus? status, DateTime? from, DateTime? to);
    Task<TodoResponseDto?> GetByIdAsync(int id);
    Task<TodoResponseDto> CreateAsync(CreateTodoDto dto);
    Task<bool> UpdateAsync(int id, UpdateTodoDto dto);
    Task<bool> DeleteAsync(int id);
}

public class TodoService(IUnitOfWork uow) : ITodoService
{
    public async Task<IEnumerable<TodoResponseDto>> GetAllAsync(TodoStatus? status, DateTime? from, DateTime? to)
        => (await uow.Todos.GetAllAsync(status, from, to)).Select(ToDto);

    public async Task<TodoResponseDto?> GetByIdAsync(int id)
    {
        var item = await uow.Todos.GetByIdAsync(id);
        return item is null ? null : ToDto(item);
    }

    public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
    {
        var item = new TodoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            DueDate = dto.DueDate
        };
        await uow.Todos.AddAsync(item);
        await uow.CommitAsync();
        return ToDto(item);
    }

    public async Task<bool> UpdateAsync(int id, UpdateTodoDto dto)
    {
        var item = await uow.Todos.GetByIdAsync(id);
        if (item is null) return false;
        item.Title = dto.Title; item.Description = dto.Description;
        item.Status = dto.Status; item.DueDate = dto.DueDate;
        uow.Todos.Update(item);
        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await uow.Todos.GetByIdAsync(id);
        if (item is null) return false;
        uow.Todos.Remove(item);
        await uow.CommitAsync();
        return true;
    }

    private static TodoResponseDto ToDto(TodoItem t)
        => new(t.Id, t.Title, t.Description, t.Status, t.DueDate);
}