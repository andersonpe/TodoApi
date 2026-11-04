using System.ComponentModel.DataAnnotations;
using TodoApi.Domain.Enumerators;

namespace TodoApi.Application.Dtos.Todo
{
    public record UpdateTodoDto(
        [Required, StringLength(200)] string Titulo,
        [StringLength(1000)] string? Descricao,
        TodoStatus Status,
        DateTime DataDeVencimento);
}
