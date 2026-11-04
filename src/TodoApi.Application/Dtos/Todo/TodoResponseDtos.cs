using TodoApi.Domain.Enumerators;

namespace TodoApi.Application.Dtos.Todo
{
    public record TodoResponseDto(
        int Id, 
        string Title, 
        string? Descricao, 
        TodoStatus Status, 
        DateTime DataDeVencimento);
}