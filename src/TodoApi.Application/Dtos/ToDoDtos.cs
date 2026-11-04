using System.ComponentModel.DataAnnotations;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;
namespace TodoApi.Application.Dtos;

public record CreateTodoDto(
    [Required, StringLength(200)] string Title,
    [StringLength(1000)] string? Description,
    TodoStatus Status,
    DateTime DueDate);

public record UpdateTodoDto(
    [Required, StringLength(200)] string Title,
    [StringLength(1000)] string? Description,
    TodoStatus Status,
    DateTime DueDate);

public record TodoResponseDto(int Id, string Title, string? Description, TodoStatus Status, DateTime DueDate);