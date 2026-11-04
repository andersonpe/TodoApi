using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;
using TodoApi.Domain.Interfaces;

namespace TodoApi.Infrastructure.Repositories
{
    public class TodoRepository(AppDbContext db) : ITodoRepository
    {
        public async Task<TodoItem?> GetByIdAsync(int id) => await db.Todos.FindAsync(id);

        public async Task<IEnumerable<TodoItem>> GetAllAsync(TodoStatus? status, DateTime? dataDeVencimento)
        {
            var query = db.Todos.AsNoTracking().AsQueryable();
            if (status.HasValue) query = query.Where(t => t.Status == status);
            if (dataDeVencimento.HasValue) query = query.Where(t => t.DataDeVencimento.Equals(dataDeVencimento));
            return await query.OrderBy(t => t.DataDeVencimento).ToListAsync();
        }

        public async Task AddAsync(TodoItem item) => await db.Todos.AddAsync(item);
        public void Update(TodoItem item) => db.Todos.Update(item);
        public void Remove(TodoItem item) => db.Todos.Remove(item);
    }
}
