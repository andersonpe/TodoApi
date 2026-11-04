using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.Dtos.Todo;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Services;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;
namespace TodoApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(ITodoService service) : ControllerBase
{
    /// <summary>Lista tarefas com filtros opcionais por status e data de vencimento.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoResponseDto>>> GetAll(
        [FromQuery] TodoStatus? status, [FromQuery] DateTime? dataVencimento)
        => Ok(await service.GetAllAsync(status, dataVencimento));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoResponseDto>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TodoResponseDto>> Create(CreateTodoDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTodoDto dto)
        => await service.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await service.DeleteAsync(id) ? NoContent() : NotFound();
}