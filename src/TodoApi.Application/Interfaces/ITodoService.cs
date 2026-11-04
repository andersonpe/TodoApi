using TodoApi.Application.Dtos.Todo;
using TodoApi.Domain.Enumerators;

namespace TodoApi.Application.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoResponseDto>> GetAllAsync(TodoStatus? status, DateTime? dataDeVencimento);
        Task<TodoResponseDto?> GetByIdAsync(int id);
        Task<TodoResponseDto> CreateAsync(CreateTodoDto dto);
        Task<bool> UpdateAsync(int id, UpdateTodoDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
